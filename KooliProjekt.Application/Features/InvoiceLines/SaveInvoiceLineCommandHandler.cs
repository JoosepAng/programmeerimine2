using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class SaveInvoiceLineCommandHandler
        : IRequestHandler<SaveInvoiceLineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveInvoiceLineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveInvoiceLineCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            if (!await _dbContext.Invoices.AnyAsync(
                    invoice => invoice.invoice_id == request.InvoiceId,
                    cancellationToken))
            {
                return result.AddPropertyError(
                    "InvoiceId",
                    "Sellise ID-ga arvet ei leitud.");
            }

            InvoiceLine line;

            if (request.Id == 0)
            {
                line = new InvoiceLine();
                _dbContext.InvoiceLines.Add(line);
            }
            else
            {
                line = await _dbContext.InvoiceLines.FindAsync(
                    new object[] { request.Id }, cancellationToken);

                if (line == null)
                {
                    return result.AddError($"InvoiceLine ID-ga {request.Id} ei leitud.");
                }
            }

            line.invoice_id = request.InvoiceId;
            line.description = request.Description;
            line.quantity = request.Quantity;
            line.unit_price = request.UnitPrice;
            line.amount = request.Amount;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
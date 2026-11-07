using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class DeleteInvoiceCommandHandler
        : IRequestHandler<DeleteInvoiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteInvoiceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            DeleteInvoiceCommand request,
            CancellationToken cancellationToken)
        {
            await _dbContext.InvoiceLines
                .Where(line => line.invoice_id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.Invoices
                .Where(invoice => invoice.invoice_id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLineQueryHandler
        : IRequestHandler<GetInvoiceLineQuery, OperationResult<InvoiceLineDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceLineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<InvoiceLineDto>> Handle(
            GetInvoiceLineQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceLineDto>();

            result.Value = await _dbContext.InvoiceLines
                .AsNoTracking()
                .Where(line => line.invoice_line_id == request.Id)
                .Select(line => new InvoiceLineDto
                {
                    Id = line.invoice_line_id,
                    InvoiceId = line.invoice_id,
                    Description = line.description,
                    Quantity = line.quantity,
                    UnitPrice = line.unit_price,
                    Amount = line.amount
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class ListInvoiceLinesQueryHandler
        : IRequestHandler<ListInvoiceLinesQuery, OperationResult<PagedResult<InvoiceLineDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoiceLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceLineDto>>> Handle(
            ListInvoiceLinesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<InvoiceLineDto>>();

            result.Value = await _dbContext.InvoiceLines
                .AsNoTracking()
                .OrderBy(line => line.invoice_line_id)
                .Select(line => new InvoiceLineDto
                {
                    Id = line.invoice_line_id,
                    InvoiceId = line.invoice_id,
                    Description = line.description,
                    Quantity = line.quantity,
                    UnitPrice = line.unit_price,
                    Amount = line.amount
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
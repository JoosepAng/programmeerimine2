using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoicesQueryHandler
        : IRequestHandler<ListInvoicesQuery, OperationResult<PagedResult<InvoiceDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoicesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceDto>>> Handle(
            ListInvoicesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<InvoiceDto>>();

            result.Value = await _dbContext.Invoices
                .AsNoTracking()
                .OrderBy(invoice => invoice.invoice_id)
                .Select(invoice => new InvoiceDto
                {
                    Id = invoice.invoice_id,
                    BookingId = invoice.booking_id,
                    UserId = invoice.user_id,
                    CreatedAt = invoice.created_at,
                    TotalAmount = invoice.total_amount,
                    PaymentStatus = invoice.payment_status,
                    MarkedPaidByUserId = invoice.marked_paid_by_user_id,
                    MarkedPaidAt = invoice.marked_paid_at
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
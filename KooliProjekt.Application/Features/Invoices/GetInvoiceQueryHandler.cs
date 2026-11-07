using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler
        : IRequestHandler<GetInvoiceQuery, OperationResult<InvoiceDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<InvoiceDto>> Handle(
            GetInvoiceQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceDto>();

            result.Value = await _dbContext.Invoices
                .AsNoTracking()
                .Where(invoice => invoice.invoice_id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
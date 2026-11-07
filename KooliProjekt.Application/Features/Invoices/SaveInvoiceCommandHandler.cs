using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandHandler
        : IRequestHandler<SaveInvoiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveInvoiceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveInvoiceCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            if (!await _dbContext.Bookings.AnyAsync(
                    booking => booking.booking_id == request.BookingId,
                    cancellationToken))
            {
                return result.AddPropertyError("BookingId", "Sellise ID-ga broneeringut ei leitud.");
            }

            if (!await _dbContext.Users.AnyAsync(
                    user => user.user_id == request.UserId,
                    cancellationToken))
            {
                return result.AddPropertyError("UserId", "Sellise ID-ga kasutajat ei leitud.");
            }

            Invoice invoice;

            if (request.Id == 0)
            {
                invoice = new Invoice();
                _dbContext.Invoices.Add(invoice);
            }
            else
            {
                invoice = await _dbContext.Invoices.FindAsync(
                    new object[] { request.Id }, cancellationToken);

                if (invoice == null)
                {
                    return result.AddError($"Invoice ID-ga {request.Id} ei leitud.");
                }
            }

            invoice.booking_id = request.BookingId;
            invoice.user_id = request.UserId;
            invoice.created_at = request.CreatedAt;
            invoice.total_amount = request.TotalAmount;
            invoice.payment_status = request.PaymentStatus;
            invoice.marked_paid_by_user_id = request.MarkedPaidByUserId;
            invoice.marked_paid_at = request.MarkedPaidAt;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandValidator
        : AbstractValidator<SaveInvoiceCommand>
    {
        public SaveInvoiceCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.BookingId)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Bookings.AnyAsync(
                        booking => booking.booking_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga broneeringut ei leitud.");

            RuleFor(command => command.UserId)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Users.AnyAsync(
                        user => user.user_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga kasutajat ei leitud.");

            RuleFor(command => command.CreatedAt)
                .GreaterThan(System.DateTime.MinValue);

            RuleFor(command => command.TotalAmount)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.PaymentStatus)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(command => command.MarkedPaidByUserId)
                .GreaterThanOrEqualTo(0)
                .MustAsync(async (id, cancellationToken) =>
                    id == 0 || await dbContext.Users.AnyAsync(
                    user => user.user_id == id,
                    cancellationToken))
                 .WithMessage("Maksmise märkinud kasutajat ei leitud.");
        }
    }
}
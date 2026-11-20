using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class DeleteBookingCommandValidator
        : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Bookings.AnyAsync(
                        booking => booking.booking_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga broneeringut ei leitud.");

            RuleFor(command => command.Id)
                .MustAsync(async (id, cancellationToken) =>
                    !await dbContext.Invoices.AnyAsync(
                        invoice => invoice.booking_id == id,
                        cancellationToken))
                .WithMessage("Broneeringut ei saa kustutada, sest sellega on seotud arve.");
        }
    }
}
using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class DeleteUserCommandValidator
        : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Users.AnyAsync(
                        user => user.user_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga kasutajat ei leitud.");

            RuleFor(command => command.Id)
                .MustAsync(async (id, cancellationToken) =>
                    !await dbContext.Bookings.AnyAsync(
                        booking => booking.user_id == id,
                        cancellationToken)
                    && !await dbContext.Invoices.AnyAsync(
                        invoice => invoice.user_id == id,
                        cancellationToken)
                    && !await dbContext.Invoices.AnyAsync(
                        invoice => invoice.marked_paid_by_user_id == id,
                        cancellationToken))
                .WithMessage("Kasutajat ei saa kustutada, sest ta on seotud broneeringu või arvega.");
        }
    }
}
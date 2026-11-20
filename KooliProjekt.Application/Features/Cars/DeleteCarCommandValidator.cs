using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class DeleteCarCommandValidator
        : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Cars.AnyAsync(
                        car => car.car_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga autot ei leitud.");
            RuleFor(command => command.Id)
               .MustAsync(async (id, cancellationToken) =>
                   !await dbContext.Bookings.AnyAsync(
                       booking => booking.car_id == id,
                       cancellationToken))
               .WithMessage("Autot ei saa kustutada, sest sellega on seotud broneering.");
        }
    }
}
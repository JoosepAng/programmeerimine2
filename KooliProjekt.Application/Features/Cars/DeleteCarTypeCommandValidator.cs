using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class DeleteCarTypeCommandValidator
        : AbstractValidator<DeleteCarTypeCommand>
    {
        public DeleteCarTypeCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.CarTypes.AnyAsync(
                        type => type.car_type_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga autotüüpi ei leitud.");

            RuleFor(command => command.Id)
                .MustAsync(async (id, cancellationToken) =>
                    !await dbContext.Cars.AnyAsync(
                        car => car.car_type_id == id,
                        cancellationToken))
                .WithMessage("Autotüüpi ei saa kustutada, sest seda kasutab vähemalt üks auto.");
        }
    }
}
using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommandValidator : AbstractValidator<SaveCarCommand>
    {
        public SaveCarCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.RegistrationNo)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(command => command.Make)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(command => command.Model)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(command => command.CarTypeId)
                .GreaterThan(0)
                .MustAsync((carTypeId, cancellationToken) =>
                    dbContext.CarTypes.AnyAsync(
                        type => type.car_type_id == carTypeId,
                        cancellationToken))
                .WithMessage("Sellise ID-ga autotüüpi ei leitud.");

            RuleFor(command => command.TimeRate)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.KilometerRate)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Status)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
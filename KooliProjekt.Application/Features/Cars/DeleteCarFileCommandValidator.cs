using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Cars
{
    public class DeleteCarFileCommandValidator
        : AbstractValidator<DeleteCarFileCommand>
    {
        public DeleteCarFileCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.CarId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((carId, cancellationToken) =>
                    dbContext.Cars.AnyAsync(
                        car => car.car_id == carId,
                        cancellationToken))
                .WithMessage("Sellise ID-ga autot ei leitud.");

            RuleFor(command => command.FileName)
                .NotEmpty()
                .Must(fileName =>
                    Path.GetFileName(fileName) == fileName)
                .WithMessage("Failinimi ei sobi.");
        }
    }
}
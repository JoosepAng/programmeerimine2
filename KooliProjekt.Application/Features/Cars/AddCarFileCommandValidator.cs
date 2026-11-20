using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Cars
{
    public class AddCarFileCommandValidator : AbstractValidator<AddCarFileCommand>
    {
        public AddCarFileCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.CarId)
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

            RuleFor(command => command.InputStream)
                .NotNull()
                .Must(stream => stream != Stream.Null)
                .WithMessage("Faili sisu puudub.");
        }
    }
}

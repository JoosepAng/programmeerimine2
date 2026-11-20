using System.IO;
using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarFileQueryValidator
        : AbstractValidator<GetCarFileQuery>
    {
        public GetCarFileQueryValidator(ApplicationDbContext dbContext)
        {
            RuleFor(query => query.CarId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((carId, cancellationToken) =>
                    dbContext.Cars.AnyAsync(
                        car => car.car_id == carId,
                        cancellationToken))
                .WithMessage("Sellise ID-ga autot ei leitud.");

            RuleFor(query => query.FileName)
                .NotEmpty()
                .Must(fileName =>
                    Path.GetFileName(fileName) == fileName)
                .WithMessage("Failinimi ei sobi.");
        }
    }
}
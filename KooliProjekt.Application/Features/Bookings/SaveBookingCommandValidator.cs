using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class SaveBookingCommandValidator
        : AbstractValidator<SaveBookingCommand>
    {
        public SaveBookingCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.UserId)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Users.AnyAsync(
                        user => user.user_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga kasutajat ei leitud.");

            RuleFor(command => command.CarId)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Cars.AnyAsync(
                        car => car.car_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga autot ei leitud.");

            RuleFor(command => command.StartTime)
                .GreaterThan(System.DateTime.MinValue);

            RuleFor(command => command.EndTime)
                .GreaterThan(command => command.StartTime)
                .WithMessage("Broneeringu lõppaeg peab olema algusajast hilisem.");

            RuleFor(command => command.StartingOdometer)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.EndingOdometer)
                .GreaterThanOrEqualTo(command => command.StartingOdometer)
                .WithMessage("Lõpp-läbisõit ei tohi olla alg-läbisõidust väiksem.");

            RuleFor(command => command.TimeRateAtBooking)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.KilometerRateAtBooking)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Status)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class SaveBookingCommandHandler
        : IRequestHandler<SaveBookingCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveBookingCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveBookingCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            if (!await _dbContext.Users.AnyAsync(
                    user => user.user_id == request.UserId, cancellationToken))
            {
                return result.AddPropertyError("UserId", "Sellise ID-ga kasutajat ei leitud.");
            }

            if (!await _dbContext.Cars.AnyAsync(
                    car => car.car_id == request.CarId, cancellationToken))
            {
                return result.AddPropertyError("CarId", "Sellise ID-ga autot ei leitud.");
            }

            Booking booking;

            if (request.Id == 0)
            {
                booking = new Booking();
                _dbContext.Bookings.Add(booking);
            }
            else
            {
                booking = await _dbContext.Bookings.FindAsync(
                    new object[] { request.Id }, cancellationToken);

                if (booking == null)
                {
                    return result.AddError($"Booking ID-ga {request.Id} ei leitud.");
                }
            }

            booking.user_id = request.UserId;
            booking.car_id = request.CarId;
            booking.start_time = request.StartTime;
            booking.end_time = request.EndTime;
            booking.starting_odometer = request.StartingOdometer;
            booking.ending_odometer = request.EndingOdometer;
            booking.time_rate_at_booking = request.TimeRateAtBooking;
            booking.kilometer_rate_at_booking = request.KilometerRateAtBooking;
            booking.status = request.Status;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class ListBookingsQueryHandler
        : IRequestHandler<ListBookingsQuery, OperationResult<PagedResult<BookingDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListBookingsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<BookingDto>>> Handle(
            ListBookingsQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<BookingDto>>();

            result.Value = await _dbContext.Bookings
                .AsNoTracking()
                .OrderBy(booking => booking.booking_id)
                .Select(booking => new BookingDto
                {
                    Id = booking.booking_id,
                    UserId = booking.user_id,
                    CarId = booking.car_id,
                    StartTime = booking.start_time,
                    EndTime = booking.end_time,
                    StartingOdometer = booking.starting_odometer,
                    EndingOdometer = booking.ending_odometer,
                    TimeRateAtBooking = booking.time_rate_at_booking,
                    KilometerRateAtBooking = booking.kilometer_rate_at_booking,
                    Status = booking.status
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
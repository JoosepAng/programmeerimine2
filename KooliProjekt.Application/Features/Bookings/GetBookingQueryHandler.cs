using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Bookings
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetBookingQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking = await _dbContext.Bookings.FindAsync(new object[] { request.Id }, cancellationToken);
            return booking;
        }
    }
}

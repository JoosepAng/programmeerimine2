using System.Threading;
using System.Threading.Tasks;
using MediatR;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, Car>
    {
        private readonly ApplicationDbContext _context;

        public GetCarQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Car> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FindAsync(new object[] { request.Id }, cancellationToken);
            return car;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQueryHandler : IRequestHandler<GetCarTypeQuery, CarType>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarTypeQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarType> Handle(GetCarTypeQuery request, CancellationToken cancellationToken)
        {
            var list = await _dbContext.CarTypes.FindAsync(request.Id, cancellationToken);

            return list;
        }
    }
}

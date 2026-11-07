using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQueryHandler
        : IRequestHandler<GetCarTypeQuery, OperationResult<CarTypeDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarTypeQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<CarTypeDto>> Handle(
            GetCarTypeQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<CarTypeDto>();

            result.Value = await _dbContext.CarTypes
                .AsNoTracking()
                .Where(carType => carType.car_type_id == request.Id)
                .Select(carType => new CarTypeDto
                {
                    Id = carType.car_type_id,
                    Name = carType.name,
                    Description = carType.description
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
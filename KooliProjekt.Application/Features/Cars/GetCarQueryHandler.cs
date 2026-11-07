using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, OperationResult<CarDto>>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetCarQueryHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<OperationResult<CarDto>> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CarDto>();

            result.Value = await _dbcontext.Cars
                .AsNoTracking()
                .Where(car => car.car_id == request.Id)
                .Select(car => new CarDto
                {
                Id = car.car_id,
                RegistrationNo = car.registration_no,
                Make = car.make,
                Model = car.model,
                CarTypeId = car.car_type_id,
                TimeRate = car.time_rate,
                KilometerRate = car.kilometer_rate,
                Status = car.status
            })
                 .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}

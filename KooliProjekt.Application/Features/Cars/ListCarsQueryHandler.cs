using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarsQueryHandler : IRequestHandler<ListCarsQuery, OperationResult<PagedResult<CarDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<CarDto>>> Handle(ListCarsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<CarDto>>();

            result.Value = await _dbContext.Cars
                .AsNoTracking()
                .OrderBy(car => car.car_id)
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
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}

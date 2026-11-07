using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class ListCarTypesQueryHandler
        : IRequestHandler<ListCarTypesQuery, OperationResult<PagedResult<CarTypeDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarTypesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<CarTypeDto>>> Handle(
            ListCarTypesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<CarTypeDto>>();

            result.Value = await _dbContext.CarTypes
                .AsNoTracking()
                .OrderBy(carType => carType.car_type_id)
                .Select(carType => new CarTypeDto
                {
                    Id = carType.car_type_id,
                    Name = carType.name,
                    Description = carType.description
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
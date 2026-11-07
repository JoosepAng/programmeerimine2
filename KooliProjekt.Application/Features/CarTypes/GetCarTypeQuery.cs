using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQuery : IRequest<OperationResult<CarTypeDto>>
    {
        public int Id { get; set; }
    }
}
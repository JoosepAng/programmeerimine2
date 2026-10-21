using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQuery : IRequest<CarType>
    {
        public int Id { get; set; }
    }
}
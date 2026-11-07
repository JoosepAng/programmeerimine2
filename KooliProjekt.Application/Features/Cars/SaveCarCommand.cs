using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string RegistrationNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int CarTypeId { get; set; }
        public decimal TimeRate { get; set; }
        public decimal KilometerRate { get; set; }
        public string Status { get; set; }

    
    }
}

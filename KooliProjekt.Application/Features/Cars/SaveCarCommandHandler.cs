using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommandHandler
        : IRequestHandler<SaveCarCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveCarCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveCarCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var carTypeExists = await _dbContext.CarTypes
                .AnyAsync(type => type.car_type_id == request.CarTypeId, cancellationToken);

            if (!carTypeExists)
            {
                return result.AddPropertyError(
                    "CarTypeId",
                    $"CarType ID-ga {request.CarTypeId} ei leitud.");
            }

            Car car;

            if (request.Id == 0)
            {
                car = new Car();
                _dbContext.Cars.Add(car);
            }
            else
            {
                car = await _dbContext.Cars.FindAsync(
                    new object[] { request.Id },
                    cancellationToken);

                if (car == null)
                {
                    return result.AddError($"Car ID-ga {request.Id} ei leitud.");
                }
            }

            car.registration_no = request.RegistrationNo;
            car.make = request.Make;
            car.model = request.Model;
            car.car_type_id = request.CarTypeId;
            car.time_rate = request.TimeRate;
            car.kilometer_rate = request.KilometerRate;
            car.status = request.Status;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
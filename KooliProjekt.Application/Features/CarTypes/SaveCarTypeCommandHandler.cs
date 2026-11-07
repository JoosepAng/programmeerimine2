using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class SaveCarTypeCommandHandler
        : IRequestHandler<SaveCarTypeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveCarTypeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveCarTypeCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            CarType carType;

            if (request.Id == 0)
            {
                carType = new CarType();
                _dbContext.CarTypes.Add(carType);
            }
            else
            {
                carType = await _dbContext.CarTypes.FindAsync(
                    new object[] { request.Id },
                    cancellationToken);

                if (carType == null)
                {
                    return result.AddError($"CarType ID-ga {request.Id} ei leitud.");
                }
            }

            carType.name = request.Name;
            carType.description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class DeleteCarTypeCommandHandler
        : IRequestHandler<DeleteCarTypeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCarTypeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            DeleteCarTypeCommand request,
            CancellationToken cancellationToken)
        {
            await _dbContext.CarTypes
                .Where(carType => carType.car_type_id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
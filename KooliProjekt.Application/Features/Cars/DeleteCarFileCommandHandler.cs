using System.IO;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class DeleteCarFileCommandHandler : IRequestHandler<DeleteCarFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileClient _fileClient;

        public DeleteCarFileCommandHandler(
            ApplicationDbContext dbContext,
            IFileClient fileClient)
        {
            _dbContext = dbContext;
            _fileClient = fileClient;
        }

        public async Task<OperationResult> Handle(
            DeleteCarFileCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var carExists = await _dbContext.Cars
                .AnyAsync(car => car.car_id == request.CarId, cancellationToken);

            if (!carExists)
            {
                return result.AddPropertyError("CarId",$"Sellise ID-ga autot ei leitud.");

            }

            await _fileClient.Delete(
                request.FileName,
                FileStore.CarPhotos(request.CarId));

            return result;
        }
    }
}
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarFilesQueryHandler
        : IRequestHandler<ListCarFilesQuery, OperationResult<IList<string>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileClient _fileClient;

        public ListCarFilesQueryHandler(
            ApplicationDbContext dbContext,
            IFileClient fileClient)
        {
            _dbContext = dbContext;
            _fileClient = fileClient;
        }

        public async Task<OperationResult<IList<string>>> Handle(
            ListCarFilesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<string>>();

            var carExists = await _dbContext.Cars
                .AnyAsync(car => car.car_id == request.CarId, cancellationToken);

            if (!carExists)
            {
                return result.AddPropertyError("CarId","Sellise ID-ga autot ei leitud.");
            }

            result.Value = await _fileClient.List(
               FileStore.CarPhotos(request.CarId));

            return result;
        }
    }
}
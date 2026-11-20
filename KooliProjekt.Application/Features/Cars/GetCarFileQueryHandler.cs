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
    public class GetCarFileQueryHandler : IRequestHandler<GetCarFileQuery, OperationResult<byte[]>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileClient _fileClient;

        public GetCarFileQueryHandler(ApplicationDbContext dbContext, IFileClient fileClient)
        {
            _dbContext = dbContext;
            _fileClient = fileClient;
        }

        public async Task<OperationResult<byte[]>> Handle(GetCarFileQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<byte[]>();

            var carExists = await _dbContext.Cars.AnyAsync(car => car.car_id == request.CarId, cancellationToken);

            if (!carExists)
            {
                return result.AddPropertyError("CarId","Sellise ID-ga autot ei leitud.");
            }

            try
            {
                result.Value = await _fileClient.Get(
                    request.FileName,
                    FileStore.CarPhotos(request.CarId));
            }
            catch (FileNotFoundException)
            {
                // Tühi Value tähendab kontrolleris NotFound vastust.
            }

            return result;
        }
    }
}

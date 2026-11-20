using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class AddCarFileCommandHandler
        : IRequestHandler<AddCarFileCommand, OperationResult>
    {
        private readonly IFileClient _fileClient;

        public AddCarFileCommandHandler(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public async Task<OperationResult> Handle(
            AddCarFileCommand request,
            CancellationToken cancellationToken)
        {
            await _fileClient.Save(
                request.FileName,
                FileStore.CarPhotos(request.CarId),
                request.InputStream);

            return new OperationResult();
        }
    }
}
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.IO;

namespace KooliProjekt.Application.Features.Cars
{
    public class AddCarFileCommand : IRequest<OperationResult>
    {
        public int CarId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Stream InputStream { get; set; } = Stream.Null;
    }
}
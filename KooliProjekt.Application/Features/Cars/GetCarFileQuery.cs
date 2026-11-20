using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarFileQuery : IRequest<OperationResult<byte[]>>
    {
        public int CarId { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}

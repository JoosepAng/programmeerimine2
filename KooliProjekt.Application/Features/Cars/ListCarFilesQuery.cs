using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarFilesQuery : IRequest<OperationResult<IList<string>>>
    {
        public int CarId { get; set; }
    }
}
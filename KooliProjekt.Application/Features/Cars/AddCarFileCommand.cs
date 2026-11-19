using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.IO;

namespace KooliProjekt.Application.Features.Cars
{
    public class AddCarFileCommand : IRequest
    {
        public int CarId { get; set; }
    }
}

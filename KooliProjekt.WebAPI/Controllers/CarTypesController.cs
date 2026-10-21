using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.CarTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class CarTypesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CarTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<CarType>> List()
        {
            return await _mediator.Send(new ListCarTypesQuery());
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var carType = await _mediator.Send(new GetCarTypeQuery { Id = id });

            if (carType == null)
            {
                return NotFound();
            }

            return Ok(carType);
        }
    }
}
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Bookings;
using KooliProjekt.Application.Features.Cars;
using KooliProjekt.Application.Features.InvoiceLines;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class InvoiceLinesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceLinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10)
        {
            var query = new ListInvoiceLinesQuery
            {
                Page = page,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetInvoiceLineQuery { Id = id };
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(SaveInvoiceLineCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new DeleteInvoiceLineCommand { Id = id });
            return Result(result);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.InvoiceLines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<InvoiceLine>> List()
        {
            return await _mediator.Send(new ListInvoiceLinesQuery());
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var invoiceLine = await _mediator.Send(new GetInvoiceLineQuery { Id = id });

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return Ok(invoiceLine);
        }
    }
}
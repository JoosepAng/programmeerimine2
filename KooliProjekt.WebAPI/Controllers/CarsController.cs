using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Cars;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class CarsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10)
        {
            var query = new ListCarsQuery
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

            var query = new GetCarQuery { Id = id };
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(SaveCarCommand command)
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

            var result = await _mediator.Send(new DeleteCarCommand { Id = id });
            return Result(result);
        }

        [HttpPost("{id:int}/Files")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> AddFile(int id, IFormFile file)
        {
            if (id <= 0 || file == null || file.Length == 0)
            {
                return BadRequest();
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var result = await _mediator.Send(new AddCarFileCommand
            {
                CarId = id,
                FileName = fileName,
                InputStream = stream
            });

            if (result.HasErrors)
            {
                return Result(result);
            }

            return Ok(new { fileName });
        }

        [HttpGet("{id:int}/Files")]
        public async Task<IActionResult> ListFiles(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new ListCarFilesQuery
            {
                CarId = id
            });

            if (result.HasErrors)
            {
                return Result(result);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id:int}/Files/{fileName}")]
        public async Task<IActionResult> GetFile(int id, string fileName)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(fileName))
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetCarFileQuery
            {
                CarId = id,
                FileName = fileName
            });

            if (result.HasErrors)
            {
                return Result(result);
            }

            if (result.Value == null)
            {
                return NotFound();
            }

            return File(result.Value, "application/octet-stream", fileName);
        }

        [HttpDelete("{id:int}/Files/{fileName}")]
        public async Task<IActionResult> DeleteFile(int id, string fileName)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(fileName))
            {
                return NotFound();
            }

            var result = await _mediator.Send(new DeleteCarFileCommand
            {
                CarId = id,
                FileName = fileName
            });

            return Result(result);
        }
    }
}
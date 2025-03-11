using Application.Common.Dtos;
using Application.Features.Column.Commands.CreateColumn;
using Application.Features.Column.Commands.DeleteColumn;
using Application.Features.Column.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ColumnsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // POST api/<ColumnsController>
        [HttpPost]
        public async Task<ActionResult<Column>> Post(CreateColumnCommand command)
        {
            var column = await _mediator.Send(command);

            return Ok(column);
        }
        
        // GET: api/<ColumnsController>
        [HttpGet("{projectId}")]
        public async Task<IEnumerable<ColumnDto>> GetColumnsByProjectId(Guid projectId)
        {
            var query = new GetColumnsByProjectIdQuery(projectId);
            var columns = await _mediator.Send(query);

            return columns;
        }
        
        // DELETE api/<ColumnsController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteColumnCommand(id);

            await _mediator.Send(command);
            return NoContent();
        }
    }
}

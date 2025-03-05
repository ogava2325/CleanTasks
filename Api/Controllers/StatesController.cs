using Application.Common.Dtos;
using Application.Features.State.Commands.CreateState;
using Application.Features.State.Commands.DeleteState;
using Application.Features.State.Commands.UpdateState;
using Application.Features.State.Queries.GetStateByCardId;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController(IMediator mediator) : ControllerBase
    {
        // GET: api/<StatesController>
        [HttpGet("{cardId}")]
        public async Task<StateDto> GetStateByCardId(Guid cardId)
        {
            var query = new GetStateByCardIdQuery(cardId);
            var state = await mediator.Send(query);
            return state;
        }
        
        // POST api/<StatesController>
        [HttpPost]
        public async Task<ActionResult<State>> Post(CreateStateCommand command)
        {
            var state = await mediator.Send(command);
            
            return Ok(state);
        }

        // PUT api/<StatesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateStateCommand command)
        {
            if (id != command.CardId)
            {
                return BadRequest();
            }

            await mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<StatesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteStateCommand(id);
            await mediator.Send(command);
            return NoContent();
        }
    }
}

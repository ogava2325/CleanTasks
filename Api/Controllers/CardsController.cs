using System.Security.Claims;
using Application.Common.Dtos;
using Application.Features.Card.Commands.CreateCard;
using Application.Features.Card.Commands.UpdateCard;
using Application.Features.Card.Queries.GetAllCards;
using Application.Features.Card.Queries.GetCardsByColumnId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CardsController>
        [HttpGet("{columnId}")]
        public async Task<IEnumerable<CardDto>> GetCardsByColumnId(Guid columnId)
        {
            var query = new GetCardsByColumnIdQuery(columnId);
            var cards = await _mediator.Send(query);

            return cards;
        }
        
        // GET: api/<CardsController>
        [HttpGet]
        public async Task<IEnumerable<CardDto>> GetAllCards()
        {
            var query = new GetAllCardsQuery();
            var cards = await _mediator.Send(query);

            return cards;
        }

        // POST api/<CardsController>
        [HttpPost]
        public async Task<ActionResult<CardDto>> Post(CreateCardCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            command.UserId = Guid.Parse(userId);
            
            var card = await _mediator.Send(command);

            return Ok(card);
        }
        
        // PUT api/<CardsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateCardCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
    

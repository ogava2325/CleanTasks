using Application.Common.Dtos;
using Application.Features.Stats.Queries.GetStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController(IMediator mediator) : ControllerBase
    {
        // GET: api/<StatsController>
        [HttpGet("{userId:guid}")]
        public async Task<StatsDto> Get(Guid userId)
        {
            var query = new GetStatsQuery(userId);
            
            return await mediator.Send(query);
        }
    }
}

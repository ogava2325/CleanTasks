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
        [HttpGet]
        public async Task<StatsDto> Get()
        {
            var query = new GetStatsQuery();
            
            return await mediator.Send(query);
        }
    }
}

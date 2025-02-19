using Application.Common.Dtos;
using Application.Features.Project.Commands.CreateProject;
using Application.Features.Project.Queries.GetProjectsByUserId;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProjectsController>
        [HttpGet("{userId}")]
        public async Task<IEnumerable<ProjectDto>> GetProjectsByUserId(Guid userId)
        {
            var query = new GetProjectsByUserIdQuery(userId);
            var projects = await _mediator.Send(query);

            return projects;
        }
        
        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<ActionResult<Project>> Post(CreateProjectCommand command)
        {
            var project = await _mediator.Send(command);

            return Ok(project);
        }
    }
}

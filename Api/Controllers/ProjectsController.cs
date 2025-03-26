using Application.Common.Dtos;
using Application.Common.Models;
using Application.Features.Project.Commands.CreateProject;
using Application.Features.Project.Commands.DeleteProject;
using Application.Features.Project.Queries.GetProjectsByUserId;
using Domain.Entities;
using MediatR;
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
        [HttpGet]
        public async Task<PaginatedList<ProjectDto>> GetProjectsByUserId([FromQuery] GetProjectsByUserIdQuery query)
        {
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
        
        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

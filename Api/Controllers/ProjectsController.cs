using Api.Hubs;
using Application.Common.Dtos;
using Application.Common.Models;
using Application.Features.Project.Commands.ArchiveProject;
using Application.Features.Project.Commands.CreateProject;
using Application.Features.Project.Commands.DeleteProject;
using Application.Features.Project.Commands.RestoreProject;
using Application.Features.Project.Commands.UpdateProject;
using Application.Features.Project.Queries.GetArchivedProjectsByUserId;
using Application.Features.Project.Queries.GetProjectById;
using Application.Features.Project.Queries.GetProjectsByUserId;
using Application.Features.User.Commands.AddUserToProject;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(
        IMediator mediator,
        IAuthorizationService authorizationService,
        IHubContext<ProjectHub> hubContext)
        : ControllerBase
    {
        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<PaginatedList<ProjectDto>> GetProjectsByUserId([FromQuery] GetProjectsByUserIdQuery query)
        {
            var projects = await mediator.Send(query);

            return projects;
        }

        // GET: api/<ProjectsController>
        [HttpGet("archived")]
        public async Task<PaginatedList<ProjectDto>> GetArchivedProjectsByUserId(
            [FromQuery] GetArchivedProjectsByUserIdQuery query)
        {
            var projects = await mediator.Send(query);

            return projects;
        }

        // GET: api/<ProjectsController/{id}>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDto>> GetById(Guid id)
        {
            var query = new GetProjectByIdQuery(id);

            var project = await mediator.Send(query);
            return Ok(project);
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<ActionResult<Project>> Post(CreateProjectCommand command)
        {
            var project = await mediator.Send(command);

            return Ok(project);
        }

        // Post api/<ProjectsController/5>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, UpdateProjectCommand command)
        {
            var authResult = await authorizationService.AuthorizeAsync(User, id, PoliciesConstants.IsProjectAdmin);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (id != command.Id)
            {
                return BadRequest();
            }

            await mediator.Send(command);

            return NoContent();
        }

        // Post api/<ProjectsController/5/users>
        [HttpPost("{projectId:guid}/users")]
        public async Task<ActionResult<Project>> AddUserToProject(Guid projectId, AddUserToProjectCommand command)
        {
            var authResult =
                await authorizationService.AuthorizeAsync(User, projectId, PoliciesConstants.IsProjectAdmin);


            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (projectId != command.ProjectId)
            {
                return BadRequest();
            }

            await mediator.Send(command);
            
            await hubContext.Clients.User(command.UserId.ToString()).SendAsync("UserAdded", projectId, command.UserId);

            return NoContent();
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProjectCommand(id);
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id:guid}/archive")]
        public async Task<IActionResult> Archive(Guid id)
        {
            var authResult = await authorizationService.AuthorizeAsync(User, id, PoliciesConstants.IsProjectAdmin);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var command = new ArchiveProjectCommand(id);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var authResult = await authorizationService.AuthorizeAsync(User, id, PoliciesConstants.IsProjectAdmin);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var command = new RestoreProjectCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
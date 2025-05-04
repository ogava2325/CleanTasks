using Api.Hubs;
using Application.Common.Dtos;
using Application.Common.Models;
using Application.Features.Project.Commands.ArchiveProject;
using Application.Features.Project.Commands.CreateProject;
using Application.Features.Project.Commands.DeleteProject;
using Application.Features.Project.Commands.RestoreProject;
using Application.Features.Project.Commands.UpdateProject;
using Application.Features.Project.Commands.UpdateUserRole;
using Application.Features.Project.Queries.GetArchivedProjectsByUserId;
using Application.Features.Project.Queries.GetProjectById;
using Application.Features.Project.Queries.GetProjectMembers;
using Application.Features.Project.Queries.GetProjectsByUserId;
using Application.Features.User.Commands.AddUserToProject;
using Application.Features.User.Commands.RemoveUserFromProject;
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
    [Authorize]
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
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<ActionResult> Put(Guid id, UpdateProjectCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await mediator.Send(command);

            await hubContext
                .Clients
                .All
                .SendAsync("ProjectUpdated", id);
            
            return NoContent();
        }

        // Post api/<ProjectsController/5/users>
        [HttpPost("{id:guid}/users")]
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<ActionResult<Project>> AddUserToProject(Guid id, AddUserToProjectCommand command)
        {
            if (id != command.ProjectId)
            {
                return BadRequest();
            }

            var result = await mediator.Send(command);
            
            await hubContext.Clients.All.SendAsync("UserAdded", id);

            return Ok(result);
        }
        
        [HttpDelete("{id:guid}/users")]
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<ActionResult<Project>> RemoveUserFromProject(Guid id, RemoveUserFromProjectCommand command)
        {
            if (id != command.ProjectId)
            {
                return BadRequest();
            }
            
            var result = await mediator.Send(command);
            
            await hubContext.Clients.User(command.UserId.ToString()).SendAsync("UserRemoved", id, command.UserId);

            return Ok(result);
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProjectCommand(id);
            await mediator.Send(command);
            
            await hubContext
                .Clients
                .All
                .SendAsync("ProjectDeleted", id);
            
            return NoContent();
        }

        [HttpPut("{id:guid}/archive")]
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<IActionResult> Archive(Guid id)
        {
            var command = new ArchiveProjectCommand(id);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id:guid}/restore")]
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<IActionResult> Restore(Guid id)
        {
            var command = new RestoreProjectCommand(id);

            await mediator.Send(command);

            return NoContent();
        }
        
        [HttpPut("{id:guid}/users/{userId:guid}/role")]
        [Authorize(Policy = PoliciesConstants.IsProjectAdmin)]
        public async Task<IActionResult> ChangeUserRole(Guid id, Guid userId, UpdateUserRoleCommand command)
        {
            if (id != command.ProjectId || userId != command.UserId)
            {
                return BadRequest();
            }
            
            var result = await mediator.Send(command);

            await mediator.Send(command);

            return Ok(result);
        }
        
        // GET: api/<ProjectsController>
        [HttpGet("{projectId:guid}/users")]
        public async Task<PaginatedList<ProjectMemberModel>> GetProjectMembers(Guid projectId, [FromQuery] GetProjectMembersQuery query)
        {
            query.ProjectId = projectId;
            
            var projectMembers = await mediator.Send(query);
            
            return projectMembers;
        }
    }
}
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence;
using Application.Features.Role.Commands.CreateRole;
using Application.Features.Role.Commands.DeleteRole;
using Application.Features.Role.Commands.UpdateRole;
using Application.Features.Role.Queries.GetAllRoles;
using Application.Features.Role.Queries.GetRoleById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<RolesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var query = new GetAllRolesQuery();
            var roles = await _mediator.Send(query);
            return Ok(roles);
        }
        
        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(Guid id)
        {
            var query = new GetRoleByIdQuery(id);
            var role = await _mediator.Send(query);
            return Ok(role);
        }
        
        // POST api/<RolesController>   
        [HttpPost]
        public async Task<ActionResult<Role>> Post(CreateRoleCommand command)
        {
            var role = await _mediator.Send(command);
            return Ok(role);
        }
        
        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateRoleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }
        
        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteRoleCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

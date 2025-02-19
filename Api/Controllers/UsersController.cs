using System.Security.Claims;
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Features.User.Commands.LoginUser;
using Application.Features.User.Commands.RegisterUser;
using Application.Features.User.Queries.GetAllUsers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        public UsersController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        // GET api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var query = new GetAllUsersQuery();
            
            var users = await _mediator.Send(query);
            return Ok(users);
        }
        
        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand userCommand)
        {
            var userId = await _mediator.Send(userCommand);
            return Ok(userId);
        }

        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<IActionResult> Post(LoginUserCommand userCommand)
        {
            var token = await _mediator.Send(userCommand);
            
            return Ok(token);
        }
        
        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetUserRoles()
        {
            var userId = GetUserId();

            var roles = await _userRepository.GetRolesAsync(userId);

            if (!roles.Any())
            {
                return NotFound(new { message = "No roles found for the user in this project" });
            }

            return Ok(new { UserId = userId, Roles = roles });
        }
        
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}

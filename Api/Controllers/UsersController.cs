using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Features.User.Commands.LoginUser;
using Application.Features.User.Commands.RegisterUser;
using Application.Features.User.Queries.GetAllUsers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
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
            
            HttpContext.Response.Cookies.Append("tasty-cookies", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTimeOffset.Now.AddHours(1),
            });
            
            return Ok(token);
        }
    }
}

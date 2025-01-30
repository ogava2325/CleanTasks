using MediatR;

namespace Application.Features.User.Commands.LoginUser;

public record LoginUserCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
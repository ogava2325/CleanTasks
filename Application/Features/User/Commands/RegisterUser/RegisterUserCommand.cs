using Application.Common.Abstraction;
using MediatR;

namespace Application.Features.User.Commands.RegisterUser;

public record RegisterUserCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
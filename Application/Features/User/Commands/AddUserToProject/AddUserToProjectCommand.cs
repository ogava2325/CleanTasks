using Application.Common.Abstraction;
using MediatR;

namespace Application.Features.User.Commands.AddUserToProject;

public class AddUserToProjectCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public Guid ProjectId { get; set; }
    public string Role { get; set; }
}
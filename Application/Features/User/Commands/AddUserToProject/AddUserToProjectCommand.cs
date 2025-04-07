using MediatR;

namespace Application.Features.User.Commands.AddUserToProject;

public class AddUserToProjectCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
}
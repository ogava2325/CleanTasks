using Application.Common.Abstraction;
using MediatR;

namespace Application.Features.User.Commands.RemoveUserFromProject;

public class RemoveUserFromProjectCommand : IRequest<Result<string>>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid CurrentUserId { get; set; }
}

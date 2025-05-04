using Application.Common.Abstraction;
using MediatR;

namespace Application.Features.Project.Commands.UpdateUserRole;

public class UpdateUserRoleCommand : IRequest<Result<string>>
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }
}
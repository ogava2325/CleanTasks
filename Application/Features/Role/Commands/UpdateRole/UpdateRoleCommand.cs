using MediatR;

namespace Application.Features.Role.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
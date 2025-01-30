using MediatR;

namespace Application.Features.Role.Commands.CreateRole;

public record CreateRoleCommand : IRequest<Unit>
{
    public string Name { get; init; }
}
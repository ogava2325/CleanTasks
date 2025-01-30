using MediatR;

namespace Application.Features.Role.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id) : IRequest;
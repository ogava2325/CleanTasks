using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Role.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>;

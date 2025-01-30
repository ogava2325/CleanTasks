using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Role.Queries.GetRoleById;

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleDto>
{
    public Guid Id { get; set; } = Id;
}
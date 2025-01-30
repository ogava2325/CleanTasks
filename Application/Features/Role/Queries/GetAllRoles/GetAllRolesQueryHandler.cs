using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Role.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(
    IMapper mapper,
    IRoleRepository roleRepository)
    : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetAllAsync();
        return mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Role.Queries.GetRoleById;

public class GetRoleByIdQueryHandler(
    IMapper mapper,
    IRoleRepository roleRepository)
    : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(role);

        return mapper.Map<RoleDto>(role);
    }
}
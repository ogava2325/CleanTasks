using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Role.Commands.UpdateRole;

public class UpdateRoleCommandHandler(
    IMapper mapper,
    IRoleRepository roleRepository)
    : IRequestHandler<UpdateRoleCommand>
{
    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(role);
        
        mapper.Map(request, role);
        
        await roleRepository.UpdateAsync(role);
    }
}
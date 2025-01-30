using System.Runtime.Versioning;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Role.Commands.DeleteRole;

public class DeleteRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<DeleteRoleCommand>
{
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(role);
        
        await roleRepository.DeleteAsync(request.Id);
    }
}
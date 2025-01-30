using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Role.Commands.CreateRole;

public class CreateRoleCommandHandler(
    IMapper mapper,
    IRoleRepository roleRepository)
    : IRequestHandler<CreateRoleCommand, Unit>
{
    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = mapper.Map<Domain.Entities.Role>(request);
        
        await roleRepository.AddAsync(role);
        
        return Unit.Value;
    }
}
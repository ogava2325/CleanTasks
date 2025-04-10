using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using Domain.Constants;
using MediatR;

namespace Application.Features.Project.Commands.CreateProject;

public class CreateProjectCommandHandler(
    IMapper mapper,
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : IRequestHandler<CreateProjectCommand, Unit>
{
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Domain.Entities.Project>(request);
        var roleId = await roleRepository.GetRoleIdByNameAsync(RoleConstants.ProjectAdmin);
         
        await projectRepository.CreateProjectAsync(project, request.UserId, roleId);
        
        return Unit.Value;
    }
}
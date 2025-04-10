using Application.Common.Interfaces.Persistence.Repositories;
using Domain.Constants;
using MediatR;

namespace Application.Features.User.Commands.AddUserToProject;

public class AddUserToProjectCommandHandler(
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : IRequestHandler<AddUserToProjectCommand>
{
    public async Task Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
    {
        var existingProjectMember = await projectRepository.IsUserInProjectAsync(request.ProjectId, request.UserId);
        
        if (existingProjectMember)
        {
            return;
        }
        
        var roleId = await roleRepository.GetRoleIdByNameAsync(RoleConstants.User);
        
        await projectRepository.AddUserToProjectAsync(request.ProjectId, request.UserId, roleId);
    }
}
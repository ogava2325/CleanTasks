using Application.Common.Abstraction;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.User.Commands.AddUserToProject;

public class AddUserToProjectCommandHandler(
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : IRequestHandler<AddUserToProjectCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
    {
        var userId = await projectRepository.GetUserIdByEmailAsync(request.Email);

        if (userId == Guid.Empty)
        {
            return Result<string>.Failure("User with provided email does not exist.");
        }
        
        var existingProjectMember = await projectRepository.IsUserInProjectAsync(request.ProjectId, userId);
        
        if (existingProjectMember)
        {
            return Result<string>.Failure("User is already a member of the project.");
        }
        
        var roleId = await roleRepository.GetRoleIdByNameAsync(request.Role);

        if (roleId == Guid.Empty)
        {
            return Result<string>.Failure("Role does not exist.");
        }
        
        await projectRepository.AddUserToProjectAsync(request.ProjectId, userId, roleId);
        return Result<string>.Success("User added to project successfully.");
    }
}
using Application.Common.Abstraction;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Project.Commands.UpdateUserRole;

public class UpdateUserRoleCommandHandler(
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : IRequestHandler<UpdateUserRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var existingProjectMember = await projectRepository.IsUserInProjectAsync(request.ProjectId, request.UserId);

        if (!existingProjectMember)
        {
            return Result<string>.Failure("User is not a member of the project.");
        }
        
        var roleId = await roleRepository.GetRoleIdByNameAsync(request.Role);

        if (roleId == Guid.Empty)
        {
            return Result<string>.Failure("Role does not exist.");
        }

        await projectRepository.UpdateUserRoleAsync(request.ProjectId, request.UserId, roleId);
        return Result<string>.Success("User role updated successfully.");
    }
}
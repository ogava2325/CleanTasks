using System.Security.Claims;
using Application.Common.Interfaces.Persistence.Repositories;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorization;

public class ProjectAdminHandler(
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : AuthorizationHandler<ProjectAdminRequirement, Guid>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAdminRequirement requirement, Guid resource)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        
        if(userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            context.Fail();
            return;
        }

        var roleId = await roleRepository.GetRoleIdByNameAsync(RoleConstants.ProjectAdmin);
        
        var isProjectAdmin = await projectRepository.IsProjectAdminAsync(resource, userId, roleId);
        if (isProjectAdmin)
        {
            context.Succeed(requirement);
        }
    }
}
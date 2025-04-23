using System.Security.Claims;
using Application.Common.Interfaces.Persistence.Repositories;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorization;

public class ProjectAdminHandler(
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : AuthorizationHandler<ProjectAdminRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAdminRequirement requirement)
    {
        if (!(context.Resource is HttpContext httpContext) ||
            !httpContext.Request.RouteValues.TryGetValue("id", out var rawId) ||
            !Guid.TryParse(rawId?.ToString(), out var projectId))
        {
            context.Fail();
            return;
        }

        var userIdString = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) ||
            !Guid.TryParse(userIdString, out var userId))
        {
            context.Fail();
            return;
        }

        var roleId = await roleRepository.GetRoleIdByNameAsync(RoleConstants.ProjectAdmin);
        
        var isProjectAdmin = await projectRepository.IsProjectAdminAsync(projectId, userId, roleId);

        if (isProjectAdmin)
        {
            context.Succeed(requirement);
        }
    }
}
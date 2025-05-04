using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IProjectRepository : IGenericRepository<Project, Guid>
{
    Task<(IEnumerable<Project>, int)> GetProjectsByUserIdAsync(
        Guid userId,
        PaginationParameters paginationParameters,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        bool? onlyArchived = false
    );

    Task<(IEnumerable<Project>, int)> GetArchivedProjectsByUserIdAsync(
        Guid userId,
        PaginationParameters paginationParameters,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate
    );

    Task CreateProjectAsync(Project project, Guid userId, Guid roleId);

    Task AddUserToProjectAsync(Guid projectId, Guid userId, Guid roleId);

    Task<bool> IsProjectAdminAsync(Guid projectId, Guid userId, Guid roleId);

    Task<bool> IsUserInProjectAsync(Guid projectId, Guid userId);

    Task RemoveUserFromProjectAsync(Guid projectId, Guid userId);

    Task ArchiveAsync(Guid projectId);
    
    Task RestoreAsync(Guid projectId);
    
    Task DeleteArchivedOlderThanAsync(TimeSpan ageThreshold);
    
    Task<(IEnumerable<ProjectMemberModel>, int)> GetProjectMembers(
        Guid projectId,
        PaginationParameters paginationParameters
    );

    Task<Guid> GetUserIdByEmailAsync(string email);
    
    Task UpdateUserRoleAsync(Guid projectId, Guid userId, Guid roleId);
}
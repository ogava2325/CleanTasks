using Application.Common.Interfaces.Persistence.Base;
using Application.Features.Project.Queries.GetProjectsByUserId;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IProjectRepository : IGenericRepository<Project, Guid>
{
    Task<(IEnumerable<Project>, int)> GetProjectsByUserIdAsync(
        Guid userId, 
        int pageNumber, 
        int pageSize, 
        string? searchTerm, 
        ProjectsSortBy sortBy,
        ProjectsSortOrder sortOrder,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate);
    
    Task CreateProjectAsync(Project project, Guid userId, Guid roleId);
}
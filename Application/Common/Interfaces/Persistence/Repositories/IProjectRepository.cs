using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IProjectRepository : IGenericRepository<Project, Guid>
{
    Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId);
    
    Task CreateProjectAsync(Project project, Guid userId, Guid roleId);
}
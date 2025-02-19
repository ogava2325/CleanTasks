using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository : IGenericRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email);
    
    Task<bool> IsEmailUniqueAsync(string email);
    
    Task<IEnumerable<string>> GetRolesAsync(Guid userId);
}
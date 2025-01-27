using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Persistence.Repositories;

public class UserRepository : GenericRepository<User, Guid>, IUserRepository
{
    public UserRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
    {
    }
}
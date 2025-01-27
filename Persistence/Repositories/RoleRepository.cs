using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Persistence.Repositories;

public class RoleRepository : GenericRepository<Role, Guid>, IRoleRepository
{
    public RoleRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
    {
    }
}
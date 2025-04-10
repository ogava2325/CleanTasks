using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class RoleRepository(IDbConnectionFactory dbConnectionFactory)
    : GenericRepository<Role, Guid>(dbConnectionFactory), IRoleRepository
{
    public async Task<Guid> GetRoleIdByNameAsync(string name)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        
        const string query = "SELECT Id FROM Roles WHERE Name = @Name";

        return await connection.QueryFirstOrDefaultAsync<Guid>(query, new { Name = name });
    }
}
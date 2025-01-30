using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class UserRepository(IDbConnectionFactory dbConnectionFactory)
    : GenericRepository<User, Guid>(dbConnectionFactory), IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<User> GetByEmailAsync(string email)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var user = await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email }
        );
        
        return user;
    }
    
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        var query = """
        SELECT CASE WHEN EXISTS (
            SELECT 1
            FROM Users
            WHERE Email = @Email
        ) THEN 0 ELSE 1 END
        """;

        return await connection.ExecuteScalarAsync<bool>(query, new { Email = email });
    }
}
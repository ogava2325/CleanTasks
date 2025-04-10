using System.Data;
using Dapper;
using Domain.Constants;

namespace Persistence;

public static class DbInitializer
{
    public static async Task SeedRolesAsync(IDbConnection connection)
    {
        // Check if a role exists
        const string checkQuery = "SELECT COUNT(*) FROM Roles WHERE Name = @Name";
        // Insert a new role if it doesn't exist
        const string insertQuery = "INSERT INTO Roles (Id, Name) VALUES (@Id, @Name)";

        var roles = new[]
        {
            new { Id = Guid.NewGuid(), Name = RoleConstants.GlobalAdmin },
            new { Id = Guid.NewGuid(), Name = RoleConstants.ProjectAdmin },
            new { Id = Guid.NewGuid(), Name = RoleConstants.User }
        };

        foreach (var role in roles)
        {
            var exists = await connection.ExecuteScalarAsync<int>(checkQuery, new { Name = role.Name });
            if (exists == 0)
            {
                await connection.ExecuteAsync(insertQuery, role);
            }
        }
    }
}
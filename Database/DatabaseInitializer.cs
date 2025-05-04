using Dapper;
using DbUp;
using Domain.Constants;
using Microsoft.Data.SqlClient;

namespace Database;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeAsync()
    {
        EnsureDatabase.For.SqlDatabase(_connectionString);

        var upgrader = DeployChanges.To.SqlDatabase(_connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DatabaseInitializer).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            upgrader.PerformUpgrade();
        }

        await SeedRolesAsync();
    }

    private async Task SeedRolesAsync()
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var roles = new[]
        {
            new { Id = Guid.NewGuid(), Name = RoleConstants.GlobalAdmin },
            new { Id = Guid.NewGuid(), Name = RoleConstants.ProjectAdmin },
            new { Id = Guid.NewGuid(), Name = RoleConstants.User },
            new { Id = Guid.NewGuid(), Name = RoleConstants.Viewer }
        };

        foreach (var role in roles)
        {
            var exists = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Roles WHERE Name = @Name",
                new { role.Name });

            if (exists == 0)
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Roles (Id, Name) VALUES (@Id, @Name)",
                    role);
            }
        }
    }
}
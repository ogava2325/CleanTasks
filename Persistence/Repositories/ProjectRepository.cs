using System.Runtime.InteropServices;
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class ProjectRepository(IDbConnectionFactory dbConnectionFactory)
    : GenericRepository<Project, Guid>(dbConnectionFactory), IProjectRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var query = """
                    SELECT * FROM Projects p
                    INNER JOIN Users_Projects up ON p.Id = up.ProjectId
                    WHERE up.UserId = @UserId
                    """;

        return await connection.QueryAsync<Project>(query, new { UserId = userId });
    }

    public async Task CreateProjectAsync(Project project, Guid userId, Guid roleId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string insertIntoProjectsQuery = """
                                                   INSERT INTO Projects (Title, Description, CreatedAtUtc, CreatedBy) 
                                                   OUTPUT INSERTED.Id
                                                   VALUES (@Title, @Description, @CreatedAtUtc, @CreatedBy)
                                                   """;

            const string insertIntoUsersProjectsQuery = """
                                                        INSERT INTO Users_Projects (UserId, ProjectId, RoleId)
                                                        VALUES (@UserId, @ProjectId, @RoleId);
                                                        """;

            var projectId = await connection.ExecuteScalarAsync<Guid>(
                insertIntoProjectsQuery,
                new
                {
                    Title = project.Title,
                    Description = project.Description,
                    CreatedAtUtc = DateTimeOffset.UtcNow,
                    CreatedBy = userId.ToString(),
                },
                transaction
            );

            await connection.ExecuteAsync(
                insertIntoUsersProjectsQuery,
                new
                {
                    ProjectId = projectId,
                    UserId = userId,
                    RoleId = roleId,
                },
                transaction
            );

            transaction.Commit();
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            Console.WriteLine($"Error occurred while creating project: {exception.Message}");
        }
    }
}
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

    public async Task CreateProjectAsync(Project project, Guid userId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var query = """
                    INSERT INTO Projects (Id, Title, Description, CreatedAtUtc, CreatedBy) 
                    VALUES (@Id, @Title, @Description, @CreatedAtUtc, @CreateBy)
                    """;

        connection.ExecuteAsync(query,
            new
            {
                Id = userId, Title = project.Title, Description = project.Description,
                CreatedAtUtc = project.CreatedAtUtc, CreatedBy = project.CreatedBy
            });

    }
}
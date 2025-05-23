using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Models;
using Application.Features.Project.Queries.GetProjectsByUserId;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class ProjectRepository(IDbConnectionFactory dbConnectionFactory)
    : GenericRepository<Project, Guid>(dbConnectionFactory), IProjectRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<(IEnumerable<Project>, int)> GetProjectsByUserIdAsync(
        Guid userId,
        PaginationParameters paginationParameters,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        bool? onlyArchived = false)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var sortByColumn = paginationParameters.SortBy switch
        {
            ProjectsSortBy.CreatedAtUtc => "p.CreatedAtUtc",
            ProjectsSortBy.Title => "p.Title",
            _ => "p.CreatedAtUtc"
        };

        var sortOrderString = paginationParameters.SortOrder == ProjectsSortOrder.Asc ? "ASC" : "DESC";

        var archivedCondition = onlyArchived == true
            ? "p.IsArchived = 1"
            : "p.IsArchived = 0";

        var countQuery = $"""
                          SELECT COUNT(*) 
                          FROM Projects p
                          INNER JOIN Users_Projects up ON p.Id = up.ProjectId
                          WHERE up.UserId = @UserId
                            AND {archivedCondition}
                          """;

        var dataQuery = $"""
                         SELECT
                             p.Id,
                             p.Title,
                             p.Description,
                             p.CreatedAtUtc,
                             p.CreatedBy
                         FROM Projects p
                         INNER JOIN Users_Projects up ON p.Id = up.ProjectId
                         WHERE up.UserId = @UserId
                           AND {archivedCondition}
                           AND (@SearchTerm IS NULL OR p.Title LIKE @SearchTerm)
                           AND (@StartDate IS NULL OR p.CreatedAtUtc >= @StartDate)
                           AND (@EndDate IS NULL OR p.CreatedAtUtc <= @EndDate)
                         ORDER BY {sortByColumn} {sortOrderString}
                         OFFSET @Offset ROWS
                         FETCH NEXT @PageSize ROWS ONLY
                         """;

        var parameters = new
        {
            UserId = userId,
            Offset = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize,
            PageSize = paginationParameters.PageSize,
            SearchTerm = string.IsNullOrWhiteSpace(paginationParameters.SearchTerm)
                ? null
                : $"%{paginationParameters.SearchTerm}%",
            StartDate = startDate,
            EndDate = endDate,
        };

        var projects = await connection.QueryAsync<Project>(dataQuery, parameters);
        var count = await connection.ExecuteScalarAsync<int>(countQuery,
            new { UserId = userId });

        return (projects, count);
    }

    public async Task<(IEnumerable<Project>, int)> GetArchivedProjectsByUserIdAsync(Guid userId,
        PaginationParameters paginationParameters, DateTimeOffset? startDate,
        DateTimeOffset? endDate)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var sortByColumn = paginationParameters.SortBy switch
        {
            ProjectsSortBy.CreatedAtUtc => "p.CreatedAtUtc",
            ProjectsSortBy.Title => "p.Title",
            _ => "p.CreatedAtUtc"
        };

        var sortOrderString = paginationParameters.SortOrder == ProjectsSortOrder.Asc ? "ASC" : "DESC";

        const string countQuery = $"""
                                   SELECT COUNT(*) 
                                   FROM Projects p
                                   INNER JOIN Users_Projects up ON p.Id = up.ProjectId
                                   WHERE up.UserId = @UserId
                                    AND p.IsArchived = 1
                                   """;

        var dataQuery = $"""
                         SELECT
                             p.Id,
                             p.Title,
                             p.Description,
                             p.CreatedAtUtc,
                             p.CreatedBy
                         FROM Projects p
                         INNER JOIN Users_Projects up ON p.Id = up.ProjectId
                         WHERE up.UserId = @UserId
                           AND p.IsArchived = 1
                           AND (@SearchTerm IS NULL OR p.Title LIKE @SearchTerm)
                           AND (@StartDate IS NULL OR p.CreatedAtUtc >= @StartDate)
                           AND (@EndDate IS NULL OR p.CreatedAtUtc <= @EndDate)
                         ORDER BY {sortByColumn} {sortOrderString}
                         OFFSET @Offset ROWS
                         FETCH NEXT @PageSize ROWS ONLY
                         """;

        var parameters = new
        {
            UserId = userId,
            Offset = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize,
            PageSize = paginationParameters.PageSize,
            SearchTerm = string.IsNullOrWhiteSpace(paginationParameters.SearchTerm)
                ? null
                : $"%{paginationParameters.SearchTerm}%",
            StartDate = startDate,
            EndDate = endDate,
        };

        var projects = await connection.QueryAsync<Project>(dataQuery, parameters);
        var count = await connection.ExecuteScalarAsync<int>(countQuery,
            new { UserId = userId });

        return (projects, count);
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

    public async Task AddUserToProjectAsync(Guid projectId, Guid userId, Guid roleId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string insertIntoUsersProjectsQuery = """
                                                    INSERT INTO Users_Projects (UserId, ProjectId, RoleId)
                                                    VALUES (@UserId, @ProjectId, @RoleId);
                                                    """;
        await connection.ExecuteAsync(
            insertIntoUsersProjectsQuery,
            new
            {
                ProjectId = projectId,
                UserId = userId,
                RoleId = roleId,
            }
        );
    }

    public async Task<bool> IsProjectAdminAsync(Guid projectId, Guid userId, Guid roleId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        const string query = """
                             SELECT COUNT(*)
                             FROM Users_Projects
                             WHERE ProjectId = @ProjectId
                               AND UserId = @UserId
                               AND RoleId = @RoleId
                             """;

        var count = await connection.ExecuteScalarAsync<int>(
            query,
            new
            {
                ProjectId = projectId,
                UserId = userId,
                RoleId = roleId
            }
        );

        return count > 0;
    }

    public async Task<bool> IsUserInProjectAsync(Guid projectId, Guid userId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string query = """
                             SELECT COUNT(*)
                             FROM Users_Projects
                             WHERE ProjectId = @ProjectId
                               AND UserId = @UserId
                             """;

        var count = await connection.ExecuteScalarAsync<int>(
            query,
            new
            {
                ProjectId = projectId,
                UserId = userId
            }
        );

        return count > 0;
    }

    public async Task RemoveUserFromProjectAsync(Guid projectId, Guid userId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string query = """
                             DELETE FROM Users_Projects
                             WHERE ProjectId = @ProjectId
                               AND UserId = @UserId
                             """;

        await connection.ExecuteAsync(query, new { ProkectId = projectId, UserId = userId });
    }

    public async Task ArchiveAsync(Guid projectId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string query = """
                             UPDATE Projects
                             SET IsArchived = 1,
                                 LastModifiedAtUtc = @LastModifiedAtUtc,
                                 ArchivedAt = @ArchivedAt
                             

                             WHERE Id = @ProjectId
                             """;

        await connection.ExecuteAsync(query,
            new
            {
                ProjectId = projectId,
                LastModifiedAtUtc = DateTimeOffset.UtcNow,
                ArchivedAt = DateTimeOffset.UtcNow,
            });
    }

    public async Task RestoreAsync(Guid projectId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string query = """
                             UPDATE Projects
                             SET IsArchived = 0,
                                 LastModifiedAtUtc = @LastModifiedAtUtc,
                                 ArchivedAt = NULL

                             WHERE Id = @ProjectId
                             """;

        await connection.ExecuteAsync(query,
            new
            {
                ProjectId = projectId,
                LastModifiedAtUtc = DateTimeOffset.UtcNow
            });
    }

    public async Task DeleteArchivedOlderThanAsync(TimeSpan ageThreshold)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var thresholdDate = DateTimeOffset.UtcNow - ageThreshold;

        const string query = """
                             DELETE FROM Projects
                             WHERE IsArchived = 1
                                AND ArchivedAt < @ThresholdDate;
                             """;

        await connection.ExecuteAsync(query, new { ThresholdDate = thresholdDate });
    }
}
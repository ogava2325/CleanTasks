using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;

namespace Persistence.Repositories;

public class StatsRepository(IDbConnectionFactory dbConnectionFactory)
    : IStatsRepository
{
    public async Task<StatsDto> GetAsync(Guid userId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        
        const string sql = """
                           SELECT
                               (SELECT COUNT(*) 
                                FROM Projects 
                                WHERE CreatedBy = @UserId) AS ProjectsCreatedCount,
                               
                               (SELECT COUNT(*) 
                                FROM Cards 
                                WHERE CreatedBy = @UserId) AS CardsCreatedCount,
                               
                               (SELECT COUNT(DISTINCT up.ProjectId) 
                                FROM Users_Projects up
                                WHERE up.UserId = @UserId) AS ProjectsMemberCount
                           """;
        
        return await connection.QuerySingleAsync<StatsDto>(sql, new { UserId = userId.ToString()});
    }
}
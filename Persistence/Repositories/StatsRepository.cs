using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;

namespace Persistence.Repositories;

public class StatsRepository(IDbConnectionFactory dbConnectionFactory)
    : IStatsRepository
{
    public async Task<StatsDto> GetAsync()
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql = """
                           SELECT
                           (SELECT COUNT(*) FROM Projects) AS ProjectsCount,
                           (SELECT COUNT(*) FROM Users) AS UsersCount,
                           (SELECT Count(*) FROM Cards) AS CardsCount
                           """;
        return await connection.QuerySingleAsync<StatsDto>(sql);
    }
}
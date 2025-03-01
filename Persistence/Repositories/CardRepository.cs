using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class CardRepository(IDbConnectionFactory dbConnectionFactory)
    : GenericRepository<Card, Guid>(dbConnectionFactory), ICardRepository
{
    public async Task<IEnumerable<CardDto>> GetCardsByColumnIdAsync(Guid columnId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string query = """
                                 SELECT 
                                     c.Id,
                                     c.Title,
                                     c.ColumnId,
                                     c.CreatedAtUtc,
                                     c.CreatedBy
                                 FROM Cards c
                                 WHERE c.ColumnId = @ColumnId
                             """;

        return await connection.QueryAsync<CardDto>(query, new { ColumnId = columnId });
    }
}
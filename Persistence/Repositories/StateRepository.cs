using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class StateRepository(IDbConnectionFactory dbConnectionFactory) : GenericRepository<State, Guid>(dbConnectionFactory), IStateRepository
{
    public async Task<State> GetByCardIdAsync(Guid cardId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        
        const string query = """
                                 SELECT 
                                     s.Id ,
                                     s.CardId,
                                     s.Description,
                                     s.Status,
                                     s.Priority
                                 FROM States s
                                 WHERE s.CardId = @CardId
                             """;

        return await connection.QuerySingleAsync<State>(query, new { CardId = cardId });
    }
}
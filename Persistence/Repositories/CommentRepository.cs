using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class CommentRepository(IDbConnectionFactory dbConnectionFactory) : GenericRepository<Comment, Guid>(dbConnectionFactory), ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetAllByCardIdAsync(Guid cardId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        
        const string query = """
                                 SELECT 
                                     c.Id ,
                                     c.CardId,
                                     c.UserId,
                                     c.Content,
                                     c.CreatedAtUtc,
                                     (u.FirstName + ' ' + u.LastName) AS createdBy
                                 FROM Comments c
                                 JOIN Users u on c.UserId = u.Id
                                 WHERE c.CardId = @CardId
                             """;

        return await connection.QueryAsync<Comment>(query, new { CardId = cardId });
    }
}
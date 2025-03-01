using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class ColumnRepository(IDbConnectionFactory dbConnectionFactory) : GenericRepository<Column, Guid>(dbConnectionFactory), IColumnRepository
{
    public async Task<IEnumerable<Column>> GetColumnsByProjectIdAsync(Guid projectId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        
        var query = """
                    SELECT * FROM Columns
                    WHERE ProjectId = @ProjectId
                    """;
        
        return await connection.QueryAsync<Column>(query, new { ProjectId = projectId });
    }
}
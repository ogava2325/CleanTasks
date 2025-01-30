using System.Data;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Microsoft.Data.SqlClient;

namespace Persistence;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection(CancellationToken token = default)
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}
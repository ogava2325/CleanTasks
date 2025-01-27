using System.Data;

namespace Application.Common.Interfaces.Persistence.Base;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
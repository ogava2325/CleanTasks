using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Dapper;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace Persistence.Repositories;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GenericRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        return await connection.GetListAsync<T>();
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        return await connection.GetAsync<T>(id);
    }

    public async Task AddAsync(T entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        await connection.InsertAsync<TId, T>(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        await connection.UpdateAsync(entity);
    }

    public async Task DeleteAsync(TId id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        await connection.DeleteAsync<T>(id);
    }
}
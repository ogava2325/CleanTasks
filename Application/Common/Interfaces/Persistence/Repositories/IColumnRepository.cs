using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IColumnRepository : IGenericRepository<Column, Guid>
{
    Task<IEnumerable<Column>> GetColumnsByProjectIdAsync(Guid projectId);
}
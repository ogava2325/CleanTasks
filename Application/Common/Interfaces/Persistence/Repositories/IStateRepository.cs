using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IStateRepository : IGenericRepository<State, Guid>
{ 
    Task<State> GetByCardIdAsync(Guid cardId);
}
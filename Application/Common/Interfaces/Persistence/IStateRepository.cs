using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces.Persistence;

public interface IStateRepository : IGenericRepository<State, Guid>
{
    
}
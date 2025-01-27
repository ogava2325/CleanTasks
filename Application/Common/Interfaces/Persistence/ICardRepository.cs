using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ICardRepository : IGenericRepository<Card, Guid>
{
    
}
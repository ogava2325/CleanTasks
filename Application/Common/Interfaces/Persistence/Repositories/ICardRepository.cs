using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface ICardRepository : IGenericRepository<Card, Guid>
{
    Task<IEnumerable<CardDto>> GetCardsByColumnIdAsync(Guid columnId);
}
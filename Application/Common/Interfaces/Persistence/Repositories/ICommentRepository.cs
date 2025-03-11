using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface ICommentRepository : IGenericRepository<Comment, Guid>
{
    Task<IEnumerable<Comment>> GetAllByCardIdAsync(Guid cardId);
}
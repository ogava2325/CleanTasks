using Application.Common.Interfaces.Persistence.Base;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IActivityRepository : IGenericRepository<Activity, Guid>
{
    
}
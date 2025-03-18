using Application.Common.Dtos;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IStatsRepository
{
    Task<StatsDto> GetAsync();
}
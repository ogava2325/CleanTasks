using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Stats.Queries.GetStats;

public class GetStatsQueryHandler(
    IStatsRepository statsRepository) 
    : IRequestHandler<GetStatsQuery, StatsDto>
{
    public async Task<StatsDto> Handle(GetStatsQuery request, CancellationToken cancellationToken)
    {
        return await statsRepository.GetAsync();
    }
}
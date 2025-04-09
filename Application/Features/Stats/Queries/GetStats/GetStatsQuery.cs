using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Stats.Queries.GetStats;

public record GetStatsQuery(Guid UserId) : IRequest<StatsDto>;
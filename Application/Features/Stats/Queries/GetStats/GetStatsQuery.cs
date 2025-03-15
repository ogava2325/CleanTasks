using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Stats.Queries.GetStats;

public record GetStatsQuery : IRequest<StatsDto>;
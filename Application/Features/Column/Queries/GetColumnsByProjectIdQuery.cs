using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Column.Queries;

public record GetColumnsByProjectIdQuery(Guid ProjectId) : IRequest<IEnumerable<ColumnDto>>;
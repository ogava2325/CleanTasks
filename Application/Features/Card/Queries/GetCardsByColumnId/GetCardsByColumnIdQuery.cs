using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Card.Queries.GetCardsByColumnId;

public record GetCardsByColumnIdQuery(Guid ColumnId) : IRequest<IEnumerable<CardDto>>;

using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Card.Queries.GetAllCards;

public class GetAllCardsQuery : IRequest<IEnumerable<CardDto>>;
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Features.Card.Queries.GetCardsByColumnId;
using AutoMapper;
using MediatR;

namespace Application.Features.Card.Queries.GetAllCards;

public class GetAllCardsQueryHandler(
    IMapper mapper,
    ICardRepository cardRepository) 
    : IRequestHandler<GetAllCardsQuery, IEnumerable<CardDto>>
{
    public async Task<IEnumerable<CardDto>> Handle(GetAllCardsQuery request, CancellationToken cancellationToken)
    {
        var cards = await cardRepository.GetAllAsync();
        
        return mapper.Map<IEnumerable<CardDto>>(cards);
    }
}

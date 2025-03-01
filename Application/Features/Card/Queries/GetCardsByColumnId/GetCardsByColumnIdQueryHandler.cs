using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Features.Column.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Card.Queries.GetCardsByColumnId;

public class GetCardsByColumnIdQueryHandler(
    IMapper mapper,
    ICardRepository cardRepository) 
    : IRequestHandler<GetCardsByColumnIdQuery, IEnumerable<CardDto>>
{
    public async Task<IEnumerable<CardDto>> Handle(GetCardsByColumnIdQuery request, CancellationToken cancellationToken)
    {
        var cards = await cardRepository.GetCardsByColumnIdAsync(request.ColumnId);
        
        return mapper.Map<IEnumerable<CardDto>>(cards);
    }
}
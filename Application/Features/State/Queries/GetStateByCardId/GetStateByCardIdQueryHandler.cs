using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.State.Queries.GetStateByCardId;

public class GetStateByCardIdQueryHandler(
    IMapper mapper,
    IStateRepository stateRepository) 
    : IRequestHandler<GetStateByCardIdQuery, StateDto>
{
    public async Task<StateDto> Handle(GetStateByCardIdQuery request, CancellationToken cancellationToken)
    {
        var state = await stateRepository.GetByCardIdAsync(request.CardId);
        
        ArgumentNullException.ThrowIfNull(state);
        
        return mapper.Map<StateDto>(state);
    }
}
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Card.Commands.CreateCard;

public class CreateCardCommandHandler(
    IMapper mapper,
    ICardRepository cardRepository) 
    : IRequestHandler<CreateCardCommand, Unit>
{
    public async Task<Unit> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var card = mapper.Map<Domain.Entities.Card>(request);
        card.CreatedAtUtc = DateTimeOffset.UtcNow;
        card.CreatedBy = "some user";
        
        await cardRepository.AddAsync(card);
        
        return Unit.Value;
    }
}
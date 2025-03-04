using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Card.Commands.CreateCard;

public class CreateCardCommandHandler(
    IMapper mapper,
    ICardRepository cardRepository) 
    : IRequestHandler<CreateCardCommand, Guid>
{
    public async Task<Guid> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var card = mapper.Map<Domain.Entities.Card>(request);
        
        card.CreatedAtUtc = DateTimeOffset.UtcNow;
        card.CreatedBy = "some user";
        
        return await cardRepository.AddAsync(card);
    }
}
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Card.Commands.UpdateCard;

public class UpdateCardCommandHandler(
    IMapper mapper,
    ICardRepository repository
    ) : IRequestHandler<UpdateCardCommand>
{
    public async Task Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        var card = await repository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(card);
        
        mapper.Map(request, card);
        
        card.LastModifiedAtUtc = DateTimeOffset.UtcNow;
        card.LastModifiedBy = "some user";
        
        await repository.UpdateAsync(card);
    }
}
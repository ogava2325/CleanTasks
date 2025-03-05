using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.State.Commands.UpdateState;

public class UpdateStateCommandHandler(
    IMapper mapper,
    IStateRepository stateRepository) 
    : IRequestHandler<UpdateStateCommand>
{
    public async Task Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        var state = await stateRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(state);
        
        mapper.Map(request, state);

        state.LastModifiedAtUtc = DateTimeOffset.UtcNow;
        state.LastModifiedBy = "some user";
        
        await stateRepository.UpdateAsync(state);
    }
}
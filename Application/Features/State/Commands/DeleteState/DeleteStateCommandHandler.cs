using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.State.Commands.DeleteState;

public class DeleteStateCommandHandler(
    IStateRepository stateRepository) 
    : IRequestHandler<DeleteStateCommand>
{
    public async Task Handle(DeleteStateCommand request, CancellationToken cancellationToken)
    {
        var state = await stateRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(state);
        
        await stateRepository.DeleteAsync(request.Id);
    }
}
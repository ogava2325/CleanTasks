using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.State.Commands.CreateState;

public class CreateStateCommandHandler(
    IMapper mapper,
    IStateRepository stateRepository) 
    : IRequestHandler<CreateStateCommand, Unit>
{
    public async Task<Unit> Handle(CreateStateCommand request, CancellationToken cancellationToken)
    {
        var state = mapper.Map<Domain.Entities.State>(request);

        state.CreatedAtUtc = DateTimeOffset.UtcNow;
        state.CreatedBy = "some user";
        
        await stateRepository.AddAsync(state);

        return Unit.Value;
    }
}
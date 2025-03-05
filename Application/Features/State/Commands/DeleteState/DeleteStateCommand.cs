using MediatR;

namespace Application.Features.State.Commands.DeleteState;

public record DeleteStateCommand(Guid Id) : IRequest;
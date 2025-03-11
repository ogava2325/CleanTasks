using MediatR;

namespace Application.Features.Column.Commands.DeleteColumn;

public record DeleteColumnCommand(Guid Id) : IRequest;

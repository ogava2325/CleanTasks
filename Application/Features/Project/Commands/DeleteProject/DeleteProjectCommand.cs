using MediatR;

namespace Application.Features.Project.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : IRequest;
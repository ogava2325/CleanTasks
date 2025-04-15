using MediatR;

namespace Application.Features.Project.Commands.RestoreProject;

public record RestoreProjectCommand(Guid ProjectId) : IRequest;
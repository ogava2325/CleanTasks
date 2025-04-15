using MediatR;

namespace Application.Features.Project.Commands.ArchiveProject;

public record ArchiveProjectCommand(Guid ProjectId) : IRequest;
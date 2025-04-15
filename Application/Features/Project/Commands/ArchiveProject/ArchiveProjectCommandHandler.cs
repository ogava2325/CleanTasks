using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Project.Commands.ArchiveProject;

public class ArchiveProjectCommandHandler(
    IProjectRepository projectRepository) 
    : IRequestHandler<ArchiveProjectCommand>
{
    public async Task Handle(ArchiveProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId);

        ArgumentNullException.ThrowIfNull(project);
        
        await projectRepository.ArchiveAsync(request.ProjectId);
    }
}
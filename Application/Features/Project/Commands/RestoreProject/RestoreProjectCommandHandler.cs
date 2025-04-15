using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Project.Commands.RestoreProject;

public class RestoreProjectCommandHandler(
    IProjectRepository projectRepository) 
    : IRequestHandler<RestoreProjectCommand>
{
    public async Task Handle(RestoreProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId);

        ArgumentNullException.ThrowIfNull(project);
        
        await projectRepository.RestoreAsync(request.ProjectId);
    }
}
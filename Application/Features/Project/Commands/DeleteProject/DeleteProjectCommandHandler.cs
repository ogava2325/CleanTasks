using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Project.Commands.DeleteProject;

public class DeleteProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var role = await projectRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(role);
        
        await projectRepository.DeleteAsync(request.Id);
    }
}
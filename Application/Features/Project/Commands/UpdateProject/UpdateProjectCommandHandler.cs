using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Project.Commands.UpdateProject;

public class UpdateProjectCommandHandler(
    IMapper mapper,
    IProjectRepository projectRepository) 
    : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(project);
        
        mapper.Map(request, project);
        
        project.LastModifiedAtUtc = DateTimeOffset.UtcNow;
        project.LastModifiedBy = request.UserId.ToString();
        
        await projectRepository.UpdateAsync(project);
    }
}
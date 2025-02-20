using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Project.Commands.CreateProject;

public class CreateProjectCommandHandler(
    IMapper mapper,
    IProjectRepository projectRepository) 
    : IRequestHandler<CreateProjectCommand, Unit>
{
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Domain.Entities.Project>(request);

        await projectRepository.CreateProjectAsync(project, request.UserId, request.RoleId);
        
        return Unit.Value;
    }
}
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(
    IMapper mapper,
    IProjectRepository projectRepository) 
    : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);

        return mapper.Map<ProjectDto>(project);
    }
}
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectsByUserId;

public class GetProjectsByUserIdQueryHandler(
    IMapper mapper,
    IProjectRepository projectRepository)
    : IRequestHandler<GetProjectsByUserIdQuery, IEnumerable<ProjectDto>>
{
    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetProjectsByUserIdAsync(request.UserId);

        return mapper.Map<IEnumerable<ProjectDto>>(projects);
    }
}
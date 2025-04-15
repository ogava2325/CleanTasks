using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectsByUserId;

public class GetProjectsByUserIdQueryHandler(
    IMapper mapper,
    IProjectRepository projectRepository)
    : IRequestHandler<GetProjectsByUserIdQuery, PaginatedList<ProjectDto>>
{
    public async Task<PaginatedList<ProjectDto>> Handle(GetProjectsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            SearchTerm = request.SearchTerm,
            SortBy = request.SortBy,
            SortOrder = request.SortOrder
        };

        var (projects, totalCount) = await projectRepository.GetProjectsByUserIdAsync(
            request.UserId,
            paginationParameters,
            request.StartDate,
            request.EndDate
        );

        var projectsDto = mapper.Map<List<ProjectDto>>(projects);

        return new PaginatedList<ProjectDto>(projectsDto, totalCount, request.PageNumber, request.PageSize);
    }
}
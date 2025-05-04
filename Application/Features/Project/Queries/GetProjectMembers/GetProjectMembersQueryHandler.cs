using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectMembers;

public class GetProjectMembersQueryHandler(
    IProjectRepository projectRepository)
    : IRequestHandler<GetProjectMembersQuery, PaginatedList<ProjectMemberModel>>
{
    public async Task<PaginatedList<ProjectMemberModel>> Handle(GetProjectMembersQuery request,
        CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            SearchTerm = request.SearchTerm,
        };

        var (users, totalCount) = await projectRepository.GetProjectMembers(
            request.ProjectId,
            paginationParameters
        );

        var usersList = users.ToList();
        
        return new PaginatedList<ProjectMemberModel>(usersList, totalCount, request.PageNumber, request.PageSize);
    }
}
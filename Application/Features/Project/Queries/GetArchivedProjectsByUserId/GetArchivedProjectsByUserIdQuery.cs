using Application.Common.Dtos;
using Application.Common.Models;
using Application.Features.Project.Queries.GetProjectsByUserId;
using MediatR;

namespace Application.Features.Project.Queries.GetArchivedProjectsByUserId;

public class GetArchivedProjectsByUserIdQuery : IRequest<PaginatedList<ProjectDto>>
{
    public Guid UserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public ProjectsSortBy SortBy { get; set; }
    public ProjectsSortOrder SortOrder { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}

using Application.Common.Models;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectMembers;

public class GetProjectMembersQuery : IRequest<PaginatedList<ProjectMemberModel>>
{
    public Guid ProjectId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
}

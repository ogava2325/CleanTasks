using Application.Features.Project.Queries.GetProjectsByUserId;

namespace Application.Common.Models;

public class PaginationParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public ProjectsSortBy SortBy { get; set; }
    public ProjectsSortOrder SortOrder { get; set; }
}
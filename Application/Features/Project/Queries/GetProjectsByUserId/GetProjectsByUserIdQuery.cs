using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectsByUserId;

public record GetProjectsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ProjectDto>>;
using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Project.Queries.GetProjectById;

public record GetProjectByIdQuery(Guid Id) : IRequest<ProjectDto>;
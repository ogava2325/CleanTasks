using MediatR;

namespace Application.Features.Project.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
}
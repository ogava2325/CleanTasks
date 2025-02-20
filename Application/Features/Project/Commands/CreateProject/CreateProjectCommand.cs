using MediatR;

namespace Application.Features.Project.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Unit>
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}
using MediatR;

namespace Application.Features.Column.Commands.CreateColumn;

public class CreateColumnCommand : IRequest<Unit>
{
    public string Title { get; set; }
    public Guid ProjectId { get; set; }
}
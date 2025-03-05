using Domain.Enums;
using MediatR;

namespace Application.Features.State.Commands.CreateState;

public class CreateStateCommand : IRequest<Unit>
{
    public string Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Guid CardId { get; set; }
}
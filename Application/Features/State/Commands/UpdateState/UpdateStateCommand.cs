using Domain.Enums;
using MediatR;

namespace Application.Features.State.Commands.UpdateState;

public class UpdateStateCommand : IRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Guid CardId { get; set; }
}
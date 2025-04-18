using Domain.Enums;

namespace Application.Common.Dtos;

public class StateDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Guid CardId { get; set; }
}
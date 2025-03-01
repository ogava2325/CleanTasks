using Domain.Enums;

namespace Application.Common.Dtos;

public class StateDto
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public Guid ProjectId { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

[Table("States")]
public class State : BaseEntity
{
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public Guid ProjectId { get; set; }
}
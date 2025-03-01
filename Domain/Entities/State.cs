using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

[Table("States")]
public class State : BaseEntity
{
    public string Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Guid CardId { get; set; }
}
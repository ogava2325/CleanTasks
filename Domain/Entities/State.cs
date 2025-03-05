using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

[Table("States")]
public class State : BaseAuditableEntity
{
    public string Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public Guid CardId { get; set; }
}
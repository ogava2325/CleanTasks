using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Activity")]
public class Activity : BaseAuditableEntity
{
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public Guid CardId { get; set; }
}
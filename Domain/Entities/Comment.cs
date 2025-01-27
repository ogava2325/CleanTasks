using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Comments")]
public class Comment : BaseAuditableEntity
{
    public string Content { get; set; }
    public Guid CardId { get; set; }
    public Guid UserId { get; set; }
}
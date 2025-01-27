using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Cards")]
public class Card : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid StateId { get; set; }
}
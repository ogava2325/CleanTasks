using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Projects")]
public class Project : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
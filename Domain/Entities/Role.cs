using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Roles")]
public class Role : BaseEntity
{
    public string Name { get; set; }
}
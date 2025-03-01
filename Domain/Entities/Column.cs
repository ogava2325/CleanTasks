using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Domain.Common;

namespace Domain.Entities;
 
[Table("Columns")]
public class Column : BaseAuditableEntity
{
    public string Title { get; set; }
    public Guid ProjectId { get; set; }
}
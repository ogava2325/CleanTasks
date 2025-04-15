namespace Domain.Common;

public class ArchivableEntity : BaseAuditableEntity
{
    public bool IsArchived { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
}
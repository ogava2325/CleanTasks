using System.Security.AccessControl;
using MediatR;

namespace Application.Features.Card.Commands.UpdateCard;

public class UpdateCardCommand : IRequest
{
    public Guid Id { get; set; }
 
    public string Title { get; set; }
    
    public Guid ColumnId { get; set; }
    
    public int RowIndex { get; set; }
    
    public DateTimeOffset? LastModifiedAtUtc { get; set; }
    
    public string? LastModifiedBy { get; set; }
}

using MediatR;

namespace Application.Features.Card.Commands.CreateCard;

public class CreateCardCommand : IRequest<Unit>
{
    public string Title { get; set; }
    public Guid ColumnId { get; set; }
    
    public int RowIndex { get; set; }
}
using MediatR;

namespace Application.Features.Comment.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Unit>
{
    public Guid CardId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
}
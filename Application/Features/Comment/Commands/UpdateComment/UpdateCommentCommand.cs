using MediatR;

namespace Application.Features.Comment.Commands.UpdateComment;

public class UpdateCommentCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
}
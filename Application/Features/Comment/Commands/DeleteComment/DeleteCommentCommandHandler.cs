using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.Comment.Commands.DeleteComment;

public class DeleteCommentCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<DeleteCommentCommand>
{
    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(comment);
        
        await commentRepository.DeleteAsync(request.Id);
    }
}
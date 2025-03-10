using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.Commands.UpdateComment;

public class UpdateCommentCommandHandler(
    IMapper mapper,
    ICommentRepository commentRepository) 
    : IRequestHandler<UpdateCommentCommand>
{
    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(comment);
        
        mapper.Map(request, comment);

        comment.LastModifiedAtUtc = DateTimeOffset.UtcNow;
        comment.LastModifiedBy = "some user";
        
        await commentRepository.UpdateAsync(comment);
    }
}
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.Commands.CreateComment;

public class CreateCommentCommandHandler(
    IMapper mapper,
    ICommentRepository commentRepository) 
    : IRequestHandler<CreateCommentCommand, Unit>
{
    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = mapper.Map<Domain.Entities.Comment>(request);

        comment.CreatedAtUtc = DateTimeOffset.Now;
        comment.CreatedBy = comment.UserId.ToString();

        await commentRepository.AddAsync(comment);
        
        return Unit.Value;
    }
}
using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.Queries.GetCommentsByCardId;

public class GetCommentsByCardIdQueryHandler(
    IMapper mapper,
    ICommentRepository commentRepository) 
    : IRequestHandler<GetCommentsByCardIdQuery, IEnumerable<CommentDto>>
{
    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByCardIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetAllByCardIdAsync(request.CardId);

        return mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}
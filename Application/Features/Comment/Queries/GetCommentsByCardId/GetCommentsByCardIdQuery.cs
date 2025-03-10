using Application.Common.Dtos;
using MediatR;

namespace Application.Features.Comment.Queries.GetCommentsByCardId;

public record GetCommentsByCardIdQuery(Guid CardId) : IRequest<IEnumerable<CommentDto>>;

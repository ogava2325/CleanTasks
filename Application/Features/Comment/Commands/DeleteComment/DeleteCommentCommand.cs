using MediatR;

namespace Application.Features.Comment.Commands.DeleteComment;

public record DeleteCommentCommand(Guid Id) : IRequest;

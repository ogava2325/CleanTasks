using Application.Common.Dtos;
using MediatR;

namespace Application.Features.User.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<UserDto>;
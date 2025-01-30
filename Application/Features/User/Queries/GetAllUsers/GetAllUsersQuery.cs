using Application.Common.Dtos;
using MediatR;

namespace Application.Features.User.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;
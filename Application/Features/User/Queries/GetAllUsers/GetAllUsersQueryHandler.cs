using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(
    IMapper mapper,
    IUserRepository userRepository)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync();
        
        return mapper.Map<IEnumerable<UserDto>>(users);
    }
}
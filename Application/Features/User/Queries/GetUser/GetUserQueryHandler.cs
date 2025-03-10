using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetUser;

public class GetUserQueryHandler(
    IMapper mapper,
    IUserRepository userRepository) 
    : IRequestHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        return mapper.Map<UserDto>(user);
    }
}
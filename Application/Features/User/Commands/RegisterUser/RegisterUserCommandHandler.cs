using Application.Common.Interfaces.Auth;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IMapper mapper,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        request.Password = passwordHasher.Generate(request.Password);
        
        var user = mapper.Map<Domain.Entities.User>(request);
        
        await userRepository.AddAsync(user);
        
        return user.Id;
    }
}
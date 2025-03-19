using Application.Common.Abstraction;
using Application.Common.Interfaces.Auth;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IMapper mapper,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        
        if (existingUser is not null)
        {
            return Result<Guid>.Failure("User with provided email already exists");
        }
        
        request.Password = passwordHasher.Generate(request.Password);
        
        var user = mapper.Map<Domain.Entities.User>(request);
        
        await userRepository.AddAsync(user);
        
        return Result<Guid>.Success(user.Id);
    }
}
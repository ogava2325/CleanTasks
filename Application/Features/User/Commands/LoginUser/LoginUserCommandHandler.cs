using Application.Common.Interfaces.Auth;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.User.Commands.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenProvider jwtTokenProvider)
    : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(
        LoginUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("User with the provided email does not exist.");
        }

        var passwordVerificationResult = passwordHasher.Verify(request.Password, user.PasswordHash);
        
        if (!passwordVerificationResult)
        {
            throw new UnauthorizedAccessException("Invalid credentials provided.");
        }

        return await jwtTokenProvider.GenerateTokenAsync(user);
    }
}
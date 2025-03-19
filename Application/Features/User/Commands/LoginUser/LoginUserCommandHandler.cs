using Application.Common.Abstraction;
using Application.Common.Interfaces.Auth;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.User.Commands.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenProvider jwtTokenProvider)
    : IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        LoginUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<string>.Failure("User with provided email does not exist.");
        }

        var passwordVerificationResult = passwordHasher.Verify(request.Password, user.PasswordHash);
        
        if (!passwordVerificationResult)
        {
            return Result<string>.Failure("Invalid credentials provided.");
        }

        var token = await jwtTokenProvider.GenerateTokenAsync(user);
        
        return Result<string>.Success(token);
    }
}
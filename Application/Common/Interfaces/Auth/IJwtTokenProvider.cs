using Domain.Entities;

namespace Application.Common.Interfaces.Auth;

public interface IJwtTokenProvider
{
    Task<string> GenerateTokenAsync(User user);
}
using Domain.Entities;

namespace Application.Common.Interfaces.Auth;

public interface IJwtTokenProvider
{
    string GenerateToken(User user);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Auth;
using Application.Common.Interfaces.Persistence.Repositories;
using Domain.Entities;
using Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public class JwtTokenProvider(
    IOptions<JwtOptions> options, 
    IUserRepository userRepository) 
    : IJwtTokenProvider
{
    private readonly JwtOptions _options = options.Value;
    public async Task<string> GenerateTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes(_options.SecretKey);
        
        var roles = await userRepository.GetRolesAsync(user.Id);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName)
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpirationHours),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

        return tokenHandler.WriteToken(token);
    }
}
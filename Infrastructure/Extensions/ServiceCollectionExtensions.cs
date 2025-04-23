using System.Text;
using Application.Common.Interfaces.Auth;
using Domain.Options;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(options 
            => configuration.GetSection("JwtOptions").Bind(options));
        
        var secretKey = configuration["JwtOptions:SecretKey"];
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
                };
                
                // Extract the token from the Authorization header
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"]
                            .FirstOrDefault();
                        if (!string.IsNullOrEmpty(authHeader) &&
                            authHeader.StartsWith("Bearer "))
                        {
                            context.Token = authHeader["Bearer ".Length..].Trim();
                            return Task.CompletedTask;
                        }

                        // 2) If this is a SignalR negotiate/WebSocket request, pull from ?access_token=
                        var accessToken = context.Request.Query["access_token"]
                            .FirstOrDefault();
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/hubs/projects"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}
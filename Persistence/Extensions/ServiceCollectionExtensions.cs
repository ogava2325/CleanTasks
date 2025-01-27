using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionFactory>(_ => new DbConnectionFactory(configuration.GetConnectionString("DefaultConnection")!));
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
        
        return services;
    }
}
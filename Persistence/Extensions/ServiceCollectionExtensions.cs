using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionFactory>(_ 
            => new DbConnectionFactory(configuration.GetConnectionString("DefaultConnection")!));
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
        services.AddScoped(typeof(IProjectRepository), typeof(ProjectRepository));
        services.AddScoped(typeof(ICardRepository), typeof(CardRepository));
        services.AddScoped(typeof(IColumnRepository), typeof(ColumnRepository));
        services.AddScoped(typeof(IStateRepository), typeof(StateRepository));
        services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
        services.AddScoped(typeof(IStatsRepository), typeof(StatsRepository));
        
        return services;
    }
}
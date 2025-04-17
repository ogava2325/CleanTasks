using Application.Common.Interfaces.Persistence.Base;
using Application.Common.Interfaces.Persistence.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.Repositories;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IDbConnectionFactory>(_ 
    => new DbConnectionFactory(builder.Configuration["DbConnectionString"]!));

builder.Build().Run();
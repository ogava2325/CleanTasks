using System.Text.Json.Serialization;
using Api.Authorization;
using Api.Hubs;
using Application.Extensions;
using Database;
using Domain.Constants;
using Domain.Enums;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddSingleton(_ => new DatabaseInitializer(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<Status>());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<Priority>());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policyBuilder => policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PoliciesConstants.IsProjectAdmin, policyBuilder =>
    {
        policyBuilder.Requirements.Add(new ProjectAdminRequirement());
    });
});

builder.Services.AddScoped<IAuthorizationHandler, ProjectAdminHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHub<ProjectHub>("/hubs/projects");
app.UseCors("all");
app.UseAuthorization();
app.MapControllers();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
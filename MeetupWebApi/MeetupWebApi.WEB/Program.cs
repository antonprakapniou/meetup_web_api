using MeetupWebApi.BLL.DTO;
using MeetupWebApi.BLL.Profiles;
using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Repositories;
using MeetupWebApi.WEB.Exceptions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Serilog;
using MeetupWebApi.BLL.Interfaces;
using MeetupWebApi.BLL.Services;
using MeetupWebApi.DAL.Services;
using IdentityServer4.AccessTokenValidation;
using MeetupWebApi.WEB.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var builderConfiguration = builder.Configuration;
string connectionName = DbConnection.defaultConnection;
string connectionString = builderConfiguration
    .GetConnectionString(connectionName)
    ??throw new InvalidDbConnectionException($"Connection \"{connectionName}\" not found.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(typeof(MeetupAutoMapperProfile));
builder.Services.AddTransient<IMeetupService, MeetupService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidatorsFromAssemblyContaining<MeetupDto>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IdentityServerAuthenticationDefaults.AuthenticationScheme="Bearer"
builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        //the URL on which the IdentityServer is up and running
        options.Authority=builderConfiguration.GetValue<string>(IdentityServerConnection.Authority);
        //the name of the WebAPI resource
        options.ApiName=builderConfiguration.GetValue<string>(IdentityServerConnection.ApiName);
        //select false for the development
        options.RequireHttpsMetadata= builderConfiguration.GetValue<bool>(IdentityServerConnection.RequireHttpsMetadata);
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        SeedData.Initialize(services);
    }

    catch(Exception ex) 
    {
        logger.Error(ex,"Db not found");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    logger.Information("Starting up...");
    app.Run();
    logger.Information("Shutting down...");
}

catch (Exception ex)
{
    logger.Error(ex, "Api host terminated unexpectedly");
}
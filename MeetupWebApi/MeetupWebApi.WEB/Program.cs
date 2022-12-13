using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Repositories;
using MeetupWebApi.WEB.Exceptions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

string connectionName = Connection.defaultConnection;
string connectionString = builder
    .Configuration
    .GetConnectionString(connectionName)
    ??throw new InvalidDbConnectionException($"Connection \"{connectionName}\" not found.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddTransient<IMeetupRepository, MeetupRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
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
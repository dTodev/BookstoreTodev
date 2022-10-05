using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Extensions;
using Bookstore.HealthChecks;
using Bookstore.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddAutoMapper(typeof(Program));

// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Health Check
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<MyCustomHealthCheck>("Custom Health Check")
    .AddUrlGroup(new Uri("https://google.bg"), name: "Google Service");

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

//app.MapHealthChecks("/health");

app.RegisterHealthChecks();

app.Run();

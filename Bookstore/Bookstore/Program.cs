using System.Text;
using Bookstore.BL.BackgroundServices;
using Bookstore.BL.CommandHandlers;
using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Extensions;
using Bookstore.HealthChecks;
using Bookstore.Middleware;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(x =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        Scheme = "bearer",
        BearerFormat ="JWT",
        Name = "JWT AUthentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token in the text box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {jwtSecurityScheme, Array.Empty<string>()}
    }); 
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("View", policy =>
    {
        policy.RequireClaim("View");
        policy.RequireClaim("Test");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add Health Check
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<MyCustomHealthCheck>("Custom Health Check")
    .AddUrlGroup(new Uri("https://google.bg"), name: "Google Service");

// Add MediatR
builder.Services.AddMediatR(typeof(GetAllBooksCommandHandler).Assembly);

builder.Services.AddIdentity<UserInfo, UserRole>().AddUserStore<UserInfoStore>().AddRoleStore<UserRoleStore>();

builder.Services.AddHostedService<MyBackgroundService>();

// App Builder below
var app = builder.Build();

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

//app.MapHealthChecks("/health");

app.RegisterHealthChecks();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();

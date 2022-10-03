using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPersonRepository, PersonInMemoryRepository>();
builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IBookRepository, BookInMemoryRepository>();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IAuthorRepository, AuthorInMemoryRepository>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();


builder.Services.AddControllers();
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

app.Run();

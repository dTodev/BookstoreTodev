using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models;

namespace Bookstore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IAuthorService, AuthorService>();

            return services;
        }
    }
}

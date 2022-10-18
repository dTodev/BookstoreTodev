using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.Cache.Models;
using Bookstore.Cache.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.DL.Repositories.MongoRepositories;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models;
using Bookstore.Models.Models.Users;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;

namespace Bookstore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IEmployeesRepository, EmployeeRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoRepository>();
            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IUserInfoService, EmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<KafkaProducerService<int, Person2>>();
            services.AddHostedService<KafkaConsumerBGService<int, Person2>>();
            services.AddHostedService<KafkaConsumerService<int, BookInfo>>();
            services.AddSingleton<IPurchaseService, PurchaseService>();
            services.AddSingleton<IShoppingCartService, ShoppingCartService>();
            //services.AddSingleton<IBookService, BookService>();
            //services.AddSingleton<IAuthorService, AuthorService>();

            return services;
        }
    }
}

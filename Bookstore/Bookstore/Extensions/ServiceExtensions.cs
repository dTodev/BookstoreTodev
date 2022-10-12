﻿using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models;
using Bookstore.Models.Models.Users;

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

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IUserInfoService, EmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<KafkaProducerService<int, Person2>>();
            //services.AddSingleton<IBookService, BookService>();
            //services.AddSingleton<IAuthorService, AuthorService>();

            return services;
        }
    }
}

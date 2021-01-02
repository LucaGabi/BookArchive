﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookArchive.DAL
{
    public static class ConfigServices
    {
        public static void AddDatabase(this IServiceCollection services, string cns)
        {
            services.AddDbContext<BookArchiveDataContext>(b => b.UseSqlite(cns)
                                                    .EnableSensitiveDataLogging()
                                                .EnableDetailedErrors()
                                                .LogTo(Console.WriteLine));

            services.AddScoped(typeof(DbContext), typeof(BookArchiveDataContext));
            services.AddScoped(typeof(IBookArchiveUOW), typeof(BookArchiveUnitOfWork));

            services.AddScoped(typeof(IAuthorsRepository), typeof(AuthorsRepository));
            services.AddScoped(typeof(IBooksRepository), typeof(BooksRepository));
        }
    }
}
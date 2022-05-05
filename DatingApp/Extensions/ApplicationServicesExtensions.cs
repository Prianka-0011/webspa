﻿using DatingApp.Data;
using DatingApp.Interfaces;
using DatingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("DatabaseConnection")));
             return services;
        }
    }
}
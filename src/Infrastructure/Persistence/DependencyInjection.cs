using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistance.DbContext;
using Persistance.Jwt;
using System.Reflection;

namespace Persistence
{
    public static class DependencyInjection
    {
        private static readonly ILoggerFactory ContextLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IJwtService, JwtService>();

            services.AddDbContext<AppDbContext>(opts =>
               opts
                   .UseLoggerFactory(ContextLoggerFactory)
                   .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}

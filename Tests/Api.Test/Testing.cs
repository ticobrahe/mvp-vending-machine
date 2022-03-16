using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using Persistance.DbContext;
using Persistance.Jwt;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Test
{
    [SetUpFixture]
    public class Testing
    {
        public static IConfiguration _configuration;
        public static IServiceCollection _services;
        public static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.Development.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            _services = new ServiceCollection();
            var startup = new Startup(_configuration);
            _services.AddSingleton(s => new Mock<IHostEnvironment>().Object);
            _services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.ApplicationName == "Api"
                && w.EnvironmentName == "Development"));
            _services.AddScoped(_ => _configuration);
            _services.AddLogging();

            startup.ConfigureServices(_services);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint();
        }

        public static async Task ResetDbState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
           where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();

            IQueryable<TEntity> query = null;
            if (predicate == null) query = context.Set<TEntity>();
            else query = context.Set<TEntity>().Where(predicate);

            return await query.ToListAsync();
        }

        public static async Task AddRangeAsync<TEntity>(List<TEntity> entities)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public static async Task CreateUsers(bool withProduct)
        {
            var users = new List<Domain.Entities.User>
            {
                new Domain.Entities.User
            {
                Password = "Tester123",
                UserName = "tico",
                Role = "Buyer",
                Deposit = 50
            },
                new Domain.Entities.User
            {
                Password = "Tester123",
                UserName = "tobi",
                Role = "Seller"
            }
            };
            await AddRangeAsync(users);
            if (withProduct)
            {
                var getUsers = await GetAllAsync<Domain.Entities.User>();
                var user = getUsers.FirstOrDefault(x => x.Role == ERole.Seller.ToString());
                var product = new Product
                {
                    SellerId = user.Id,
                    ProductName = "Chocolate",
                    AmountAvailable = 4,
                    Cost = 20
                };

                await AddAsync(product);
            }
        }

        public static async Task GetAccessToken(string role)
        {
            var user = new Domain.Entities.User
            {
                Password = "Tester123",
                UserName = "tico",
                Role = role
            };

            await AddAsync(user);
            using var scope = _scopeFactory.CreateScope();
            var jwt = scope.ServiceProvider.GetService<IJwtService>();
            jwt.GenerateAsync(user);
        }
    
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }
    }
}

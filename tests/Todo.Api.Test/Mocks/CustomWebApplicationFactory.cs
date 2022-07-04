using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Todo.Api.Test.Helpers;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Contracts.Services;
using Todo.Infrastructure.Database.Context;

namespace Todo.Api.Test.Mocks
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(new string[0])
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<T>();
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var databaseService = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                var messageService = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IMessageService));

                var todoItemRepository = services.SingleOrDefault(
                    d => d.ServiceType == typeof(ITodoItemRepository));

                services.Remove(databaseService);
                services.Remove(messageService);
                services.Remove(todoItemRepository);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                services.AddSingleton<IMessageService, MessageServiceFake>();
                services.AddSingleton<ITodoItemNoSqlRepository, TodoItemNoSqlRepositoryFake>();

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<T>>>();

                    db.Database.EnsureCreated();
                    SeedDB.InitializeDbForTests(db);
                }
            });
        }
    }
}
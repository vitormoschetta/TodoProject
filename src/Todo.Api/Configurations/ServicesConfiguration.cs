using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Todo.Domain.Commands.Handlers;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Queries.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Queries.Handlers;
using Todo.Infrastructure.Database.Context;
using Todo.Infrastructure.Database.Repositories;

namespace Todo.Api.Configurations
{
    public static class ServicesConfiguration
    {
        public static void DatabaseConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            var dbConnection = new
            {
                Host = Configuration["DB_CONNECTION:HOST"],
                User = Configuration["DB_CONNECTION:USER"],
                Pass = Configuration["DB_CONNECTION:PASSWORD"],
                Name = Configuration["DB_CONNECTION:DATABASE"]
            };

            var connectionString =
                $"server={dbConnection.Host};" +
                $"user={dbConnection.User};" +
                $"password={dbConnection.Pass};" +
                $"database={dbConnection.Name}";

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(connectionString));
        }

        public static void SwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApi", Version = "v1" });
            });
        }

        public static void CorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static void RepositoriesConfig(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void CommandHandlersConfig(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemCommandHandler, TodoItemCommandHandler>();
        }

        public static void QueryHandlersConfig(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemQueryHandler, TodoItemQueryHandler>();
        }
    }
}
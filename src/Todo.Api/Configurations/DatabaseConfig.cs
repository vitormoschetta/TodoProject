using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Database.Context;

namespace Todo.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration Configuration)
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

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Database.Context;

namespace Todo.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            var dbConnection = new
            {
                Host = Configuration["DatabaseConnection:Host"],
                User = Configuration["DatabaseConnection:Username"],
                Pass = Configuration["DatabaseConnection:Password"],
                Name = Configuration["DatabaseConnection:Database"]
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
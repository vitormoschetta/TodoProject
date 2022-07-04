using Todo.Api.Configurations;
using Todo.Api.Middlewares;
using Todo.Infrastructure.Database.Models.NoSql;

namespace Todo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwagger();
            services.AddCustomCors();
            services.AddRepositories();
            services.AddCommandHandlers();
            services.AddQueryHandlers();
            services.AddServices();
            services.AddDatabase(Configuration);
            services.Configure<DatabaseNoSqlSettings>(Configuration.GetSection("DatabaseNoSqlConnection"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
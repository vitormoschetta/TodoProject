using Microsoft.OpenApi.Models;

namespace Todo.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApi", Version = "v1" });
            });
        }
    }
}
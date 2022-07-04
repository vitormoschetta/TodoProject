using Todo.Application.Commands.Handlers;
using Todo.Application.Contracts.Commands.Handlers;
using Todo.Application.Contracts.Queries.Handlers;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Contracts.Services;
using Todo.Application.Queries.Handlers;
using Todo.Infrastructure.Database.Repositories;
using Todo.Infrastructure.Services;

namespace Todo.Api.Configurations
{
    public static class CustomServices
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITodoItemNoSqlRepository, TodoItemNoSqlRepository>();
        }

        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemCommandHandler, TodoItemCommandHandler>();
        }

        public static void AddQueryHandlers(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemQueryHandler, TodoItemQueryHandler>();
        }

        public static void AddServices(this IServiceCollection services)
        {            
            services.AddSingleton<IMessageService, MessageService>();
        }
    }
}
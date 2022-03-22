using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Commands.Handlers;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Queries.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Queries.Handlers;
using Todo.Infrastructure.Database.Repositories;

namespace Todo.Api.Configurations
{
    public static class CustomServices
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemCommandHandler, TodoItemCommandHandler>();
        }

        public static void AddQueryHandlers(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemQueryHandler, TodoItemQueryHandler>();
        }
    }
}
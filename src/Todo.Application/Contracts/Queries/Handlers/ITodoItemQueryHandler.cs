using System.Linq.Expressions;
using Todo.Application.Queries.Responses;
using Todo.Domain.Entities;

namespace Todo.Application.Contracts.Queries.Handlers
{
    // Here we can apply filters, mappings, etc.
    public interface ITodoItemQueryHandler
    {
        Task<IEnumerable<TodoItemResponse>> GetAll();
        Task<TodoItemResponse> GetById(Guid id);
        Task<TodoItemResponse> Get(Expression<Func<TodoItem, bool>> predicate);
    }
}
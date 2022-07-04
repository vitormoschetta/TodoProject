using System.Linq.Expressions;
using Todo.Application.Queries.Responses;
using Todo.Domain.Entities;

namespace Todo.Application.Contracts.Repositories
{
    public interface ITodoItemNoSqlRepository
    {
        Task<List<TodoItemResponse>> GetAsync();

        Task<TodoItemResponse> GetAsync(string id);

        Task<List<TodoItemResponse>> GetAsync(Expression<Func<TodoItemResponse, bool>> predicate);

        void CreateAsync(TodoItem todoItem);

        void UpdateAsync(string id, TodoItem todoItem);

        void RemoveAsync(string id);
    }
}
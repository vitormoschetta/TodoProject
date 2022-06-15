using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Todo.Domain.Entities;

namespace Todo.Application.Contracts.Repositories
{
    public interface ITodoItemRepository
    {
        Task Add(TodoItem item);
        Task AddRange(IEnumerable<TodoItem> items);
        Task Update(TodoItem item);
        Task UpdateRange(IEnumerable<TodoItem> items);
        Task Delete(TodoItem item);
        Task Delete(Guid id);
        Task UpdateAllToDone();
        Task<IEnumerable<TodoItem>> GetAll();
        Task<IEnumerable<TodoItem>> GetMany(Expression<Func<TodoItem, bool>> predicate);
        Task<TodoItem> GetById(Guid id);
        Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate);        
        IQueryable<TodoItem> Query(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null);
        Task<bool> Exists(Guid id);
        Task<bool> Exists(string title);
    }
}
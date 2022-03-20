using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Todo.Domain.Models;

namespace Todo.Domain.Contracts.Repositories
{
    public interface ITodoItemRepository
    {
        Task Add(TodoItem item);
        Task AddRange(IEnumerable<TodoItem> items);
        Task Update(TodoItem item);
        Task UpdateRange(IEnumerable<TodoItem> items);
        Task Delete(TodoItem item);
        Task Delete(int id);
        Task<IEnumerable<TodoItem>> GetAll();
        Task<IEnumerable<TodoItem>> GetMany(Expression<Func<TodoItem, bool>> predicate);
        Task<TodoItem> GetById(int id);
        Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate);        
        IQueryable<TodoItem> Query(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null);
        Task<bool> Exists(int id);
        Task<bool> Exists(string title);
    }
}
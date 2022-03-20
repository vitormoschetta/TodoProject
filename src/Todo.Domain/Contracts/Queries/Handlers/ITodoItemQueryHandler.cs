using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Contracts.Queries.Handlers
{
    // Here we can apply filters, mappings, etc.
    public interface ITodoItemQueryHandler
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> GetById(int id);
        Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate);
    }
}
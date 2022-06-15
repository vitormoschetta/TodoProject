using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Todo.Application.Contracts.Repositories;
using Todo.Domain.Entities;
using Todo.Infrastructure.Database.Context;

namespace Todo.Infrastructure.Database.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        protected readonly AppDbContext _context;

        public TodoItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(TodoItem item)
        {
            await _context.TodoItem.AddAsync(item);
        }

        public async Task AddRange(IEnumerable<TodoItem> items)
        {
            await _context.TodoItem.AddRangeAsync(items);
        }

        public async Task Delete(TodoItem item)
        {
            _context.TodoItem.Remove(item);
            await Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            _context.TodoItem.Remove(await _context.TodoItem.FindAsync(id));
            await Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.TodoItem.FindAsync(id) != null;
        }

        public async Task<bool> Exists(string title)
        {
            return await _context.TodoItem.FirstOrDefaultAsync(x => x.Title == title) != null;
        }

        public async Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate)
        {
            return await _context.TodoItem.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItem.ToListAsync();
        }

        public async Task<TodoItem> GetById(Guid id)
        {
            return await _context.TodoItem.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetMany(Expression<Func<TodoItem, bool>> predicate)
        {
            return await _context.TodoItem.Where(predicate).ToListAsync();
        }

        public IQueryable<TodoItem> Query(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null)
        {
            IQueryable<TodoItem> query = _context.Set<TodoItem>();

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        public async Task Update(TodoItem item)
        {
            _context.TodoItem.Update(item);
            await Task.CompletedTask;
        }

        public async Task UpdateAllToDone()
        {
            await _context.TodoItem.ForEachAsync(x => x.Done = true);
        }

        public async Task UpdateRange(IEnumerable<TodoItem> items)
        {
            _context.TodoItem.UpdateRange(items);
            await Task.CompletedTask;
        }
    }
}
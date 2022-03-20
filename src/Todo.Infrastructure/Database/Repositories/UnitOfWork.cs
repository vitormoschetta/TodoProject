using System;
using System.Threading.Tasks;
using Todo.Domain.Contracts.Repositories;
using Todo.Infrastructure.Database.Context;

namespace Todo.Infrastructure.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, ITodoItemRepository todoItemRepository = null)
        {
            _context = context;
            TodoItem = (todoItemRepository != null) ? todoItemRepository : new TodoItemRepository(context);
        }

        public ITodoItemRepository TodoItem { get; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }        
    }
}
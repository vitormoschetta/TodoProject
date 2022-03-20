using System;
using System.Threading.Tasks;

namespace Todo.Domain.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItem { get; }
        Task Commit();
        Task Rollback();
    }
}
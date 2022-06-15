namespace Todo.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItem { get; }
        Task Commit();        
    }
}
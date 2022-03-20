using System.Threading.Tasks;
using Todo.Domain.Contracts.Repositories;

namespace Todo.UnitTest.Mocks
{
    public class UnitOfWorkFake : IUnitOfWork
    {
        public UnitOfWorkFake(ITodoItemRepository todoItemRepository)
        {
            TodoItem = todoItemRepository;
        }

        public ITodoItemRepository TodoItem { get; }

        public Task Commit()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
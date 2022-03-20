using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Contracts.Queries.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Models;

namespace Todo.Domain.Queries.Handlers
{
    public class TodoItemQueryHandler : ITodoItemQueryHandler
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemQueryHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
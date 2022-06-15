using System.Linq.Expressions;
using Todo.Application.Contracts.Queries.Handlers;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Queries.Responses;
using Todo.Domain.Entities;

namespace Todo.Application.Queries.Handlers
{
    public class TodoItemQueryHandler : ITodoItemQueryHandler
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemQueryHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItemResponse>> GetAll()
        {
            return from result in await _repository.GetAll()
                   select new TodoItemResponse
                   {
                       Id = result.Id,
                       Title = result.Title,
                       Done = result.Done,
                   };
        }

        public async Task<TodoItemResponse> GetById(Guid id)
        {
            var result = await _repository.GetById(id);

            if (result == null)
                return null;

            return new TodoItemResponse
            {
                Id = result.Id,
                Title = result.Title,
                Done = result.Done,
            };
        }

        public async Task<TodoItemResponse> Get(Expression<Func<TodoItem, bool>> predicate)
        {
            var result = await _repository.Get(predicate);
            return result == null ? null : new TodoItemResponse
            {
                Id = result.Id,
                Title = result.Title,
                Done = result.Done,
            };
        }
    }
}
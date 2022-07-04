using System.Linq.Expressions;
using Todo.Application.Contracts.Queries.Handlers;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Queries.Responses;

namespace Todo.Application.Queries.Handlers
{
    public class TodoItemQueryHandler : ITodoItemQueryHandler
    {
        private readonly ITodoItemNoSqlRepository _repository;

        public TodoItemQueryHandler(ITodoItemNoSqlRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItemResponse>> GetAll() =>
            await _repository.GetAsync();

        public async Task<TodoItemResponse> GetById(Guid id) =>
            await _repository.GetAsync(id.ToString());

        public async Task<IEnumerable<TodoItemResponse>> Get(Expression<Func<TodoItemResponse, bool>> predicate) =>
            await _repository.GetAsync(predicate);
    }
}
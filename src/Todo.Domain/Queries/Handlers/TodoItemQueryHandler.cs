using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<TodoItem> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate)
        {
            return await _repository.Get(predicate);
        }
    }
}
using System.Linq.Expressions;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Queries.Responses;
using Todo.Domain.Entities;

namespace Todo.Application.Test.Mocks
{
    public class TodoItemNoSqlRepositoryFake : ITodoItemNoSqlRepository
    {
        private readonly List<TodoItem> _source;

        public TodoItemNoSqlRepositoryFake()
        {
            _source = new List<TodoItem>()
            {
                new TodoItem
                {
                    Id = Guid.Parse("ddffe2dc-b162-406d-8520-429043bd9b7c"),
                    Title = "First Task",
                    Done = false
                },
                new TodoItem
                {
                    Id = Guid.Parse("0afe7382-2ac0-4ddf-a2bd-432da680b924"),
                    Title = "Second Task",
                    Done = false
                }
            };
        }

        public void CreateAsync(TodoItem todoItem)
        {
            _source.Add(todoItem);            
        }

        public Task<List<TodoItemResponse>> GetAsync()
        {
            return Task.FromResult(_source.Select(x => new TodoItemResponse
            {
                Id = x.Id,
                Title = x.Title,
                Done = x.Done
            }).ToList());
        }

        public Task<TodoItemResponse> GetAsync(string id)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id.ToString() == id);

            if (todoItem != null)
            {
                return Task.FromResult(new TodoItemResponse
                {
                    Id = todoItem.Id,
                    Title = todoItem.Title,
                    Done = todoItem.Done
                });
            }
            else
            {
                return Task.FromResult<TodoItemResponse>(null);
            }
        }

        public Task<List<TodoItemResponse>> GetAsync(Expression<Func<TodoItemResponse, bool>> predicate)
        {
            return Task.FromResult(_source.Select(x => new TodoItemResponse
            {
                Id = x.Id,
                Title = x.Title,
                Done = x.Done
            }).ToList());
        }

        public void RemoveAsync(string id)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id.ToString() == id);

            if (todoItem != null)
            {
                _source.Remove(todoItem);
            }
        }

        public void UpdateAsync(string id, TodoItem todoItem)
        {
            var todoItemToUpdate = _source.FirstOrDefault(x => x.Id.ToString() == id);

            if (todoItemToUpdate != null)
            {
                todoItemToUpdate.Title = todoItem.Title;
                todoItemToUpdate.Done = todoItem.Done;
            }
        }
    }
}
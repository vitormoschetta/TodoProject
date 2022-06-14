using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Todo.Application.Contracts.Repositories;
using Todo.Domain.Entities;

namespace Todo.Application.Test.Mocks
{
    public class TodoItemRepositoryFake : ITodoItemRepository
    {
        private readonly List<TodoItem> _source;

        public TodoItemRepositoryFake()
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

        public Task Add(TodoItem item)
        {
            _source.Add(item);
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<TodoItem> items)
        {
            _source.AddRange(items);
            return Task.CompletedTask;
        }

        public Task Delete(TodoItem item)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id == item.Id);

            if (todoItem != null)
            {
                _source.Remove(todoItem);
            }

            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id == id);

            if (todoItem != null)
            {
                _source.Remove(todoItem);
            }

            return Task.CompletedTask;
        }

        public Task<bool> Exists(Guid id)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id == id);

            if (todoItem != null)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> Exists(string title)
        {
            return Task.FromResult(_source.Where(x => x.Title == title).Any());
        }

        public Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate)
        {
            return Task.FromResult(_source.FirstOrDefault(predicate.Compile()));
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            return Task.FromResult((IEnumerable<TodoItem>)_source);
        }

        public Task<TodoItem> GetById(Guid id)
        {
            return Task.FromResult(_source.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<TodoItem>> GetMany(Expression<Func<TodoItem, bool>> predicate)
        {
            return Task.FromResult(_source.Where(predicate.Compile()));
        }

        public IQueryable<TodoItem> Query(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null)
        {
            IQueryable<TodoItem> query = _source.AsQueryable<TodoItem>();

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        public Task Update(TodoItem item)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id == item.Id);

            if (todoItem != null)
            {
                _source.Remove(todoItem);
                _source.Add(item);
            }

            return Task.CompletedTask;
        }

        public Task UpdateAllToDone()
        {
            foreach (var item in _source)
            {
                item.Done = true;
            }

            return Task.CompletedTask;
        }

        public Task UpdateRange(IEnumerable<TodoItem> items)
        {
            foreach (var item in items)
            {
                var todoItem = _source.FirstOrDefault(x => x.Id == item.Id);

                if (todoItem != null)
                {
                    _source.Remove(todoItem);
                    _source.Add(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Models;

namespace Todo.UnitTest.Mocks
{
    public class TodoItemRepositoryFake : ITodoItemRepository
    {
        private readonly List<TodoItem> _source;

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

        public Task Delete(int id)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id == id);

            if (todoItem != null)
            {
                _source.Remove(todoItem);
            }

            return Task.CompletedTask;
        }

        public Task<bool> Exists(int id)
        {
            var todoItem = _source.FirstOrDefault(x => x.Id == id);

            if (todoItem != null)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> Exists(Expression<Func<TodoItem, bool>> predicate)
        {
            return Task.FromResult(_source.Where(predicate.Compile()).Any());
        }

        public Task<TodoItem> Get(Expression<Func<TodoItem, bool>> predicate)
        {
            return Task.FromResult(_source.FirstOrDefault(predicate.Compile()));
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            return Task.FromResult((IEnumerable<TodoItem>)_source);
        }

        public Task<TodoItem> GetById(int id)
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
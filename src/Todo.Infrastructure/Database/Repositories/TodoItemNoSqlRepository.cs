using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Queries.Responses;
using Todo.Domain.Entities;
using Todo.Infrastructure.Database.Models.NoSql;

namespace Todo.Infrastructure.Database.Repositories
{
    public class TodoItemNoSqlRepository : ITodoItemNoSqlRepository
    {
        private readonly IMongoCollection<TodoItemNoSql> _collection;

        public TodoItemNoSqlRepository(IOptions<DatabaseNoSqlSettings> todoDatabaseSettings)
        {
            var mongoClient = new MongoClient(todoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(todoDatabaseSettings.Value.DatabaseName);

            if (!mongoDatabase.ListCollectionNames().ToList().Contains(todoDatabaseSettings.Value.TodoCollectionName))
                mongoDatabase.CreateCollection(todoDatabaseSettings.Value.TodoCollectionName);

            _collection = mongoDatabase.GetCollection<TodoItemNoSql>(todoDatabaseSettings.Value.TodoCollectionName);
        }

        public async Task<List<TodoItemResponse>> GetAsync()
        {
            var response = await _collection.Find(_ => true).ToListAsync();
            return response.Select(x => new TodoItemResponse(Guid.Parse(x.Id), x.Title, x.Done)).ToList();
        }

        public async Task<TodoItemResponse> GetAsync(string id)
        {
            var response = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return new TodoItemResponse(Guid.Parse(response.Id), response.Title, response.Done);
        }

        public async Task<List<TodoItemResponse>> GetAsync(Expression<Func<TodoItemResponse, bool>> predicate)
        {
            var expression = Expression.Lambda<Func<TodoItemNoSql, bool>>(predicate.Body, predicate.Parameters);
            var response = await _collection.Find(expression).ToListAsync();
            return response.Select(x => new TodoItemResponse(Guid.Parse(x.Id), x.Title, x.Done)).ToList();
        }

        public async void CreateAsync(TodoItem todoItem)
        {
            var todoItemNoSql = new TodoItemNoSql(todoItem);
            await _collection.InsertOneAsync(todoItemNoSql);
        }

        public async void UpdateAsync(string id, TodoItem todoItem)
        {
            var todoItemNoSql = new TodoItemNoSql(todoItem);
            await _collection.ReplaceOneAsync(x => x.Id == id, todoItemNoSql);
        }

        public async void RemoveAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
using MongoDB.Bson.Serialization.Attributes;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Database.Models.NoSql
{
    public class TodoItemNoSql
    {
        public TodoItemNoSql(TodoItem todoItem)
        {
            Id = todoItem.Id.ToString();
            Title = todoItem.Title;
            Done = todoItem.Done;
        }

        public TodoItemNoSql()
        { }

        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }

        public void MapTo(TodoItem todoItem)
        {
            Id = todoItem.Id.ToString();
            Title = todoItem.Title;
            Done = todoItem.Done;
        }
    }
}
using System.Collections.Generic;
using Todo.Domain.Models;
using Todo.Infrastructure.Database.Context;

namespace Todo.IntegrationTest.Helpers
{
    public static class SeedDB
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            db.TodoItem.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext db)
        {
            db.TodoItem.RemoveRange(db.TodoItem);
            InitializeDbForTests(db);
        }

        public static List<TodoItem> GetSeedingMessages()
        {
            return new List<TodoItem>()
            {
                new TodoItem(){ Id = 1, Title = "First Task", Done = false },
                new TodoItem(){ Id = 2, Title = "Second Task",  Done = true }
            };
        }
    }
}
using System;
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
                    Done = true
                }
            };
        }
    }
}
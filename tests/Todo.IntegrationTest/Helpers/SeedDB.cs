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
                    Title = "Task 01",
                    Done = false
                },
                new TodoItem
                {
                    Id = Guid.Parse("0afe7382-2ac0-4ddf-a2bd-432da680b924"),
                    Title = "Task 02",
                    Done = false
                },
                new TodoItem
                {
                    Id = Guid.Parse("05670b31-6ae5-4251-be13-c6717449df3c"),
                    Title = "Task 03",
                    Done = true
                }
            };
        }
    }
}
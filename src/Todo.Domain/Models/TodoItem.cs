using System;

namespace Todo.Domain.Models
{
    public class TodoItem
    {
        public TodoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }

        public void Update(string title, bool done)
        {
            Title = title;
            Done = done;
        }

        public void MarkAsDone()
        {
            Done = true;
        }
    }
}
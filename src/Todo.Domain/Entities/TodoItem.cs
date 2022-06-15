namespace Todo.Domain.Entities
{
    public class TodoItem
    {
        public TodoItem()
        {
            Id = Guid.NewGuid();
        }

        public TodoItem(string title, bool done)
        {
            Id = Guid.NewGuid();
            Title = title;
            Done = done;
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
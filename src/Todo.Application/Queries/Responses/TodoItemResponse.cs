namespace Todo.Application.Queries.Responses
{
    public class TodoItemResponse
    {
        public TodoItemResponse(Guid id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }

        public TodoItemResponse()
        {

        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
    }
}
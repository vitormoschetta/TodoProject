namespace Todo.Application.Queries.Responses
{
    public class TodoItemResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
    }
}
using FluentValidation.Results;
using Todo.Application.Commands.Validations;

namespace Todo.Application.Commands.Requests
{
    public class UpdateTodoItemRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }

        public ValidationResult Validate()
        {
            return new UpdateTodoItemRequestValidator().Validate(this);
        }
    }
}
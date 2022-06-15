using FluentValidation.Results;
using Todo.Application.Commands.Validations;

namespace Todo.Application.Commands.Requests
{
    public class CreateTodoItemRequest
    {
        public string Title { get; set; }
        public bool Done { get; set; }

        public ValidationResult Validate()
        {
            return new CreateTodoItemRequestValidator().Validate(this);
        }
    }
}
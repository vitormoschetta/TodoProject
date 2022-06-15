using FluentValidation.Results;
using Todo.Application.Commands.Validations;

namespace Todo.Application.Commands.Requests
{
    public class DeleteTodoItemRequest
    {
        public Guid Id { get; set; }

        public ValidationResult Validate()
        {
            return new DeleteTodoItemRequestValidator().Validate(this);
        }
    }
}
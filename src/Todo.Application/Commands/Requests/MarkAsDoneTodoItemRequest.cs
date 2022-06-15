using FluentValidation.Results;
using Todo.Application.Commands.Validations;

namespace Todo.Application.Commands.Requests
{
    public class MarkAsDoneTodoItemRequest
    {
        public Guid Id { get; set; }

        public ValidationResult Validate()
        {
            return new MarkAsDoneTodoItemRequestValidator().Validate(this);
        }
    }
}
using FluentValidation.Results;
using Todo.Domain.Commands.Validations;

namespace Todo.Domain.Commands.UpdateCommands
{
    public class TodoItemMarkAsDoneCommand
    {
        public int Id { get; set; }

        public ValidationResult Validate()
        {
            return new TodoItemMarkAsDoneCommandValidator().Validate(this);
        }
    }
}
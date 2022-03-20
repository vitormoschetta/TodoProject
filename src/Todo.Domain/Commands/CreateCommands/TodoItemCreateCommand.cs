using FluentValidation.Results;
using Todo.Domain.Commands.Validations;

namespace Todo.Domain.Commands.CreateCommands
{
    public class TodoItemCreateCommand
    {
        public string Title { get; set; }
        public bool Done { get; set; }

        public ValidationResult Validate()
        {
            return new TodoItemCreateCommandValidator().Validate(this);
        }
    }
}
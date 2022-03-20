using FluentValidation.Results;
using Todo.Domain.Commands.Validations;

namespace Todo.Domain.Commands.CreateCommands
{
    public class TodoItemCreateCommand
    {
        public bool Done { get; set; }
        public string Title { get; set; }

        public ValidationResult Validate()
        {
            return new TodoItemCreateCommandValidator().Validate(this);
        }
    }
}
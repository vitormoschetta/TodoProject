using FluentValidation.Results;
using Todo.Domain.Commands.Validations;

namespace Todo.Domain.Commands.UpdateCommands
{
    public class TodoItemUpdateCommand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }

        public ValidationResult Validate()
        {
            return new TodoItemUpdateCommandValidator().Validate(this);
        }
    }
}
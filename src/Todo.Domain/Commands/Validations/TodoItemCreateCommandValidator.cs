using FluentValidation;
using Todo.Domain.Commands.CreateCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemCreateCommandValidator : AbstractValidator<TodoItemCreateCommand>
    {
        public TodoItemCreateCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
        }
    }
}
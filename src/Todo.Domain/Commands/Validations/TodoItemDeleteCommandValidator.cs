using FluentValidation;
using Todo.Domain.Commands.DeleteCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemDeleteCommandValidator : AbstractValidator<TodoItemDeleteCommand>
    {
        public TodoItemDeleteCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
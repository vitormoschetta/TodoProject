using FluentValidation;
using Todo.Domain.Commands.UpdateCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemMarkAsDoneCommandValidator : AbstractValidator<TodoItemMarkAsDoneCommand>
    {
        public TodoItemMarkAsDoneCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
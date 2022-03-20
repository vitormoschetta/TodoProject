using FluentValidation;
using Todo.Domain.Commands.UpdateCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemUpdateCommandValidator : AbstractValidator<TodoItemUpdateCommand>
    {
        public TodoItemUpdateCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
        }
    }
}
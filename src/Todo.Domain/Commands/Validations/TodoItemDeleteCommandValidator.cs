using System;
using FluentValidation;
using Todo.Domain.Commands.DeleteCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemDeleteCommandValidator : AbstractValidator<TodoItemDeleteCommand>
    {
        public TodoItemDeleteCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(guid => Guid.TryParse(guid.ToString(), out Guid result))
                .WithMessage("Invalid Guid value");

            RuleFor(x => x.Id)
                .Must(guid => guid != Guid.Empty)
                .WithMessage("Empty Guid value");
        }
    }
}
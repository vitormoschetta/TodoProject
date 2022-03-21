using System;
using FluentValidation;
using Todo.Domain.Commands.UpdateCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemMarkAsDoneCommandValidator : AbstractValidator<TodoItemMarkAsDoneCommand>
    {
        public TodoItemMarkAsDoneCommandValidator()
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
using System;
using FluentValidation;
using Todo.Domain.Commands.UpdateCommands;

namespace Todo.Domain.Commands.Validations
{
    public class TodoItemUpdateCommandValidator : AbstractValidator<TodoItemUpdateCommand>
    {
        public TodoItemUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(guid => Guid.TryParse(guid.ToString(), out Guid result))
                .WithMessage("Invalid Guid value");

            RuleFor(x => x.Id)
                .Must(guid => guid != Guid.Empty)
                .WithMessage("Empty Guid value");

            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
        }
    }
}
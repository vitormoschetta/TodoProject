using System;
using FluentValidation.Results;
using Todo.Domain.Commands.Validations;

namespace Todo.Domain.Commands.DeleteCommands
{
    public class TodoItemDeleteCommand
    {
        public Guid Id { get; set; }

        public ValidationResult Validate()
        {
            return new TodoItemDeleteCommandValidator().Validate(this);
        }
    }
}
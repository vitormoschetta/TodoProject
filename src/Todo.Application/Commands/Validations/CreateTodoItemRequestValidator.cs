using FluentValidation;
using Todo.Application.Commands.Requests;

namespace Todo.Application.Commands.Validations
{
    public class CreateTodoItemRequestValidator : AbstractValidator<CreateTodoItemRequest>
    {
        public CreateTodoItemRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
        }
    }
}
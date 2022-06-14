using FluentValidation;
using Todo.Application.Commands.Requests;

namespace Todo.Application.Commands.Validations
{
    public class MarkAsDoneTodoItemRequestValidator : AbstractValidator<MarkAsDoneTodoItemRequest>
    {
        public MarkAsDoneTodoItemRequestValidator()
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
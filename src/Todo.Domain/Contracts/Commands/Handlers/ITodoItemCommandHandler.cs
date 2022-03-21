using System.Threading.Tasks;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Response;
using Todo.Domain.Commands.UpdateCommands;

namespace Todo.Domain.Contracts.Commands.Handlers
{
    public interface ITodoItemCommandHandler
    {
        Task<CommandResponse> Handle(TodoItemCreateCommand command);
        Task<CommandResponse> Handle(TodoItemUpdateCommand command);
        Task<CommandResponse> Handle(TodoItemDeleteCommand command);
        Task<CommandResponse> Handle(TodoItemMarkAsDoneCommand command);
        Task UpdateAllToDone();
    }
}
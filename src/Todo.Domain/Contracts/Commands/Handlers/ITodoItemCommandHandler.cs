using System.Threading.Tasks;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Response;
using Todo.Domain.Commands.UpdateCommands;

namespace Todo.Domain.Contracts.Commands.Handlers
{
    public interface ITodoItemCommandHandler
    {
        Task<GenericResponse> Handle(TodoItemCreateCommand command);
        Task<GenericResponse> Handle(TodoItemUpdateCommand command);        
        Task<GenericResponse> Handle(TodoItemDeleteCommand command);        
    }
}
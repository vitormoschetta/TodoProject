using Todo.Application.Commands.Requests;
using Todo.Application.Commands.Responses;

namespace Todo.Application.Contracts.Commands.Handlers
{
    public interface ITodoItemCommandHandler
    {
        Task<GenericResponse> Handle(CreateTodoItemRequest request);
        Task<GenericResponse> Handle(UpdateTodoItemRequest request);
        Task<GenericResponse> Handle(DeleteTodoItemRequest request);
        Task<GenericResponse> Handle(MarkAsDoneTodoItemRequest request);
        Task UpdateAllToDone();
    }
}
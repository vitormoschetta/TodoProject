using System.Net;
using Todo.UI.Blazor.Models;

namespace Todo.UI.Blazor.Services;
public interface ITodoService
{
    Task<(HttpStatusCode, IEnumerable<TodoItem>)> GetAll();
    Task<(HttpStatusCode, GenericResponse)> Add(TodoItem todo);
    Task<(HttpStatusCode, GenericResponse)> Update(TodoItem todo);
    Task<(HttpStatusCode, GenericResponse)> Delete(Guid id);
}
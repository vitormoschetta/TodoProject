using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Todo.App.Models;

namespace Todo.App.Services
{
    public interface ITodoService
    {
        Task<(HttpStatusCode, IEnumerable<TodoItem>)> GetAll();
        Task<(HttpStatusCode, GenericResponse)> Add(TodoItem todo);
        Task<(HttpStatusCode, GenericResponse)> Delete(int id);
    }
}
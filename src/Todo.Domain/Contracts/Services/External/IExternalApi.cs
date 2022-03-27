using System.Net;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Contracts.Services.External
{
    public interface IExternalApi
    {
        Task<HttpStatusCode> PostTodoItem(TodoItem todoItem);
        Task<HttpStatusCode> PutTodoItem(TodoItem todoItem);
        Task<HttpStatusCode> DeleteTodoItem(TodoItem todoItem);
    }
}
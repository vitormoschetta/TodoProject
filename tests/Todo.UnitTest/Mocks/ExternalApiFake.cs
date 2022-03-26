using System.Net;
using System.Threading.Tasks;
using Todo.Domain.Contracts.Services.External;
using Todo.Domain.Models;

namespace Todo.UnitTest.Mocks
{
    public class ExternalApiFake : IExternalApi
    {
        public async Task<HttpStatusCode> PostTodoItem(TodoItem todoItem)
        {
            return await Task.FromResult(HttpStatusCode.OK);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Contracts.Queries.Handlers
{
    public interface ITodoItemQueryHandler
    {
        Task<IEnumerable<TodoItem>> GetAll();
    }
}
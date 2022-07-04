using Microsoft.AspNetCore.Mvc;
using Todo.Application.Commands.Requests;
using Todo.Application.Commands.Responses;
using Todo.Application.Contracts.Commands.Handlers;
using Todo.Application.Contracts.Queries.Handlers;
using Todo.Application.Enums;
using Todo.Application.Queries.Responses;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ApiBaseController
    {
        private readonly ITodoItemCommandHandler _commandHandler;
        private readonly ITodoItemQueryHandler _queryHandler;


        public TodoItemController(ITodoItemCommandHandler commandHandler, ITodoItemQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create([FromBody] CreateTodoItemRequest request)
        {
            var response = await _commandHandler.Handle(request);

            return StatusCodeResponse(response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse>> Update(Guid id, [FromBody] UpdateTodoItemRequest command)
        {
            if (id != command.Id)
            {
                return StatusCodeResponse(new GenericResponse("Invalid ID", EStatusResponse.BadRequest));
            }

            var response = await _commandHandler.Handle(command);

            return StatusCodeResponse(response);
        }


        [HttpDelete()]
        public async Task<ActionResult<GenericResponse>> Delete([FromQuery] DeleteTodoItemRequest command)
        {
            var response = await _commandHandler.Handle(command);

            return StatusCodeResponse(response);
        }


        [HttpGet()]
        public async Task<IEnumerable<TodoItemResponse>> GetAll()
        {
            return await _queryHandler.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemResponse>> GetById(Guid id)
        {
            return await _queryHandler.GetById(id);
        }

        [HttpGet("GetByTitle/{title}")]
        public async Task<IEnumerable<TodoItemResponse>> GetByTitle(string title)
        {
            return await _queryHandler.Get(x => x.Title == title);
        }
    }
}
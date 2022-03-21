using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Response;
using Todo.Domain.Commands.UpdateCommands;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Queries.Handlers;
using Todo.Domain.Enums;
using Todo.Domain.Models;

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
        public async Task<ActionResult<CommandResponse>> Create([FromBody] TodoItemCreateCommand command)
        {
            var response = await _commandHandler.Handle(command);

            return CustomResponse(response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CommandResponse>> Update(Guid id, [FromBody] TodoItemUpdateCommand command)
        {
            if (id != command.Id)
            {
                return CustomResponse(new CommandResponse("Invalid ID", EOutputType.InvalidInput));
            }

            var response = await _commandHandler.Handle(command);

            return CustomResponse(response);
        }


        [HttpDelete()]
        public async Task<ActionResult<CommandResponse>> Delete([FromQuery] TodoItemDeleteCommand command)
        {
            var response = await _commandHandler.Handle(command);

            return CustomResponse(response);
        }


        [HttpGet()]
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _queryHandler.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<TodoItem> GetById(Guid id)
        {
            return await _queryHandler.GetById(id);
        }

        [HttpGet("GetByTitle/{title}")]
        public async Task<TodoItem> GetByTitle(string title)
        {
            return await _queryHandler.Get(x => x.Title == title);
        }
    }
}
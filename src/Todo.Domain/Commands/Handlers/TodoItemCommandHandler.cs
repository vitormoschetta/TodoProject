using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Responses;
using Todo.Domain.Commands.UpdateCommands;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Contracts.Services;
using Todo.Domain.Enums;
using Todo.Domain.Helpers;
using Todo.Domain.Models;

namespace Todo.Domain.Commands.Handlers
{
    public class TodoItemCommandHandler : ITodoItemCommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;
        private readonly IMessageService _messageService;

        public TodoItemCommandHandler(IUnitOfWork uow, IMessageService messageService, ILogger<TodoItemCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
            _messageService = messageService;
        }

        public async Task<CommandResponse> Handle(TodoItemCreateCommand command)
        {
            var validationCommand = command.Validate();

            if (validationCommand.IsValid == false)
            {
                return new CommandResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
            }

            if (await _uow.TodoItem.Exists(command.Title))
            {
                return new CommandResponse("TodoItem already registered!", EOutputType.BusinessValidation);
            }

            var todoItem = new TodoItem()
            {
                Title = command.Title,
                Done = command.Done
            };

            await _uow.TodoItem.Add(todoItem);
            await _uow.Commit();

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Created, todoItem);

            // Utilizando um serviço de fila tornamos a comunicação com um serviço externo assíncrono.
            // Ou seja, não seremos mais afetados pela indisponibilidade de serviços externos.
            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new CommandResponse("Created!");
        }

        public async Task<CommandResponse> Handle(TodoItemUpdateCommand command)
        {
            var validationCommand = command.Validate();

            if (validationCommand.IsValid == false)
            {
                return new CommandResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
            }

            var todoItem = await _uow.TodoItem.GetById(command.Id);

            if (todoItem is null)
            {
                return new CommandResponse("TodoItem not found", EOutputType.NotFound);
            }

            todoItem.Update(command.Title, command.Done);

            await _uow.TodoItem.Update(todoItem);
            await _uow.Commit();

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Updated, todoItem);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new CommandResponse("Updated!");
        }

        public async Task<CommandResponse> Handle(TodoItemDeleteCommand command)
        {
            var validationCommand = command.Validate();

            if (validationCommand.IsValid == false)
            {
                return new CommandResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
            }

            var todoItem = await _uow.TodoItem.GetById(command.Id);

            if (todoItem is null)
            {
                return new CommandResponse("TodoItem not found", EOutputType.NotFound);
            }

            await _uow.TodoItem.Delete(todoItem);
            await _uow.Commit();

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Deleted, todoItem);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new CommandResponse("Deleted!");
        }

        public async Task<CommandResponse> Handle(TodoItemMarkAsDoneCommand command)
        {
            var validationCommand = command.Validate();

            if (validationCommand.IsValid == false)
            {
                return new CommandResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
            }

            var todoItem = await _uow.TodoItem.GetById(command.Id);

            if (todoItem is null)
            {
                return new CommandResponse("TodoItem not found", EOutputType.NotFound);
            }

            todoItem.MarkAsDone();

            await _uow.TodoItem.Update(todoItem);
            await _uow.Commit();

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Updated, todoItem);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new CommandResponse("Mark as Done!");
        }

        public async Task UpdateAllToDone()
        {
            await _uow.TodoItem.UpdateAllToDone();
            await _uow.Commit();

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.UpdateAll);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);
        }
    }
}
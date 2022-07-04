using Microsoft.Extensions.Logging;
using Todo.Application.Commands.Requests;
using Todo.Application.Commands.Responses;
using Todo.Application.Contracts.Commands.Handlers;
using Todo.Application.Contracts.Repositories;
using Todo.Application.Contracts.Services;
using Todo.Application.Enums;
using Todo.Application.Extensions;
using Todo.Application.Helpers;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.Handlers
{
    public class TodoItemCommandHandler : ITodoItemCommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;
        private readonly IMessageService _messageService;
        private readonly ITodoItemNoSqlRepository _todoItemNoSqlRepository;

        public TodoItemCommandHandler(IUnitOfWork uow, IMessageService messageService, ILogger<TodoItemCommandHandler> logger, ITodoItemNoSqlRepository todoItemNoSqlRepository)
        {
            _uow = uow;
            _logger = logger;
            _messageService = messageService;
            _todoItemNoSqlRepository = todoItemNoSqlRepository;
        }

        public async Task<GenericResponse> Handle(CreateTodoItemRequest request)
        {
            var requestValidation = request.Validate();

            if (requestValidation.IsValid == false)
            {
                return new GenericResponse(requestValidation.BuildNotifications(), EStatusResponse.BadRequest);
            }

            if (await _uow.TodoItem.Exists(request.Title))
            {
                return new GenericResponse("TodoItem already registered!", EStatusResponse.BadRequest);
            }

            var todoItem = new TodoItem()
            {
                Title = request.Title,
                Done = request.Done
            };

            await _uow.TodoItem.Add(todoItem);
            await _uow.Commit();

            // TODO: Adicionar Eventos de domínio para lidar com gravação no banco NoSQL e envio de mensagens

            // Gravando em banco de dados NoSQL
            _todoItemNoSqlRepository.CreateAsync(todoItem);

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Created, todoItem);

            // Utilizando um serviço de fila tornamos a comunicação com um serviço externo assíncrono.
            // Ou seja, não seremos mais afetados pela indisponibilidade de serviços externos.
            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new GenericResponse("Created!");
        }

        public async Task<GenericResponse> Handle(UpdateTodoItemRequest request)
        {
            var requestValidation = request.Validate();

            if (requestValidation.IsValid == false)
            {
                return new GenericResponse(requestValidation.BuildNotifications(), EStatusResponse.BadRequest);
            }

            var todoItem = await _uow.TodoItem.GetById(request.Id);

            if (todoItem is null)
            {
                return new GenericResponse("TodoItem not found", EStatusResponse.NotFound);
            }

            todoItem.Update(request.Title, request.Done);

            await _uow.TodoItem.Update(todoItem);
            await _uow.Commit();

            _todoItemNoSqlRepository.UpdateAsync(todoItem.Id.ToString(), todoItem);

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Updated, todoItem);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new GenericResponse("Updated!");
        }

        public async Task<GenericResponse> Handle(DeleteTodoItemRequest request)
        {
            var requestValidation = request.Validate();

            if (requestValidation.IsValid == false)
            {
                return new GenericResponse(requestValidation.BuildNotifications(), EStatusResponse.BadRequest);
            }

            var todoItem = await _uow.TodoItem.GetById(request.Id);

            if (todoItem is null)
            {
                return new GenericResponse("TodoItem not found", EStatusResponse.NotFound);
            }

            await _uow.TodoItem.Delete(todoItem);
            await _uow.Commit();

            _todoItemNoSqlRepository.RemoveAsync(todoItem.Id.ToString());

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Deleted, todoItem);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new GenericResponse("Deleted!");
        }

        public async Task<GenericResponse> Handle(MarkAsDoneTodoItemRequest request)
        {
            var requestValidation = request.Validate();

            if (requestValidation.IsValid == false)
            {
                return new GenericResponse(requestValidation.BuildNotifications(), EStatusResponse.BadRequest);
            }

            var todoItem = await _uow.TodoItem.GetById(request.Id);

            if (todoItem is null)
            {
                return new GenericResponse("TodoItem not found", EStatusResponse.NotFound);
            }

            todoItem.MarkAsDone();

            await _uow.TodoItem.Update(todoItem);
            await _uow.Commit();

            var message = SerializationManager.SerializeDomainEventToJson(EMessageType.Updated, todoItem);

            _messageService.SendMessage(message);

            _logger.LogInformation(message);

            return new GenericResponse("Mark as Done!");
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
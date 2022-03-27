using System;
using System.Net;
using System.Threading.Tasks;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Responses;
using Todo.Domain.Commands.UpdateCommands;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Contracts.Services.External;
using Todo.Domain.Enums;
using Todo.Domain.Models;

namespace Todo.Domain.Commands.Handlers
{
    public class TodoItemCommandHandler : ITodoItemCommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IExternalApi _externalApi;

        public TodoItemCommandHandler(IUnitOfWork uow, IExternalApi externalApi)
        {
            _uow = uow;
            _externalApi = externalApi;
        }

        public async Task<CommandResponse> Handle(TodoItemCreateCommand command)
        {
            try
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

                var httpResponse = await _externalApi.PostTodoItem(todoItem);

                if (httpResponse != HttpStatusCode.OK)
                {
                    return new CommandResponse("não foi possível enviar para o serviço externo", EOutputType.Failure);
                }

                await _uow.TodoItem.Add(todoItem);
                await _uow.Commit();

                return new CommandResponse("Created!");
            }
            catch (Exception ex)
            {
                return new CommandResponse(ex);
            }
        }

        public async Task<CommandResponse> Handle(TodoItemUpdateCommand command)
        {
            try
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

                return new CommandResponse("Updated!");
            }
            catch (Exception ex)
            {
                return new CommandResponse(ex);
            }
        }

        public async Task<CommandResponse> Handle(TodoItemDeleteCommand command)
        {
            try
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

                return new CommandResponse("Deleted!");


            }
            catch (Exception ex)
            {
                return new CommandResponse(ex);
            }
        }

        public async Task<CommandResponse> Handle(TodoItemMarkAsDoneCommand command)
        {
            try
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

                return new CommandResponse("Mark as Done!");
            }
            catch (Exception ex)
            {
                return new CommandResponse(ex);
            }
        }

        public async Task UpdateAllToDone()
        {
            try
            {
                await _uow.TodoItem.UpdateAllToDone();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Response;
using Todo.Domain.Commands.UpdateCommands;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Enums;
using Todo.Domain.Models;

namespace Todo.Domain.Commands.Handlers
{
    public class TodoItemCommandHandler : ITodoItemCommandHandler
    {
        private readonly IUnitOfWork _uow;

        public TodoItemCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<GenericResponse> Handle(TodoItemCreateCommand command)
        {
            try
            {
                var validationCommand = command.Validate();

                if (validationCommand.IsValid == false)
                {
                    return new GenericResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
                }

                if (await _uow.TodoItem.Exists(command.Title))
                {
                    return new GenericResponse("TodoItem already registered!", EOutputType.BusinessValidation);
                }

                var todoItem = new TodoItem()
                {
                    Title = command.Title,
                    Done = command.Done
                };

                await _uow.TodoItem.Add(todoItem);
                await _uow.Commit();

                return new GenericResponse("Created!");
            }
            catch (Exception ex)
            {
                return new GenericResponse(ex);
            }
        }

        public async Task<GenericResponse> Handle(TodoItemUpdateCommand command)
        {
            try
            {
                var validationCommand = command.Validate();

                if (validationCommand.IsValid == false)
                {
                    return new GenericResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
                }

                var todoItem = await _uow.TodoItem.GetById(command.Id);

                if (todoItem is null)
                {
                    return new GenericResponse("TodoItem not found", EOutputType.NotFound);
                }

                todoItem.Update(command.Title, command.Done);

                await _uow.TodoItem.Update(todoItem);
                await _uow.Commit();

                return new GenericResponse("Updated!");
            }
            catch (Exception ex)
            {
                return new GenericResponse(ex);
            }
        }

        public async Task<GenericResponse> Handle(TodoItemDeleteCommand command)
        {
            try
            {
                var validationCommand = command.Validate();

                if (validationCommand.IsValid == false)
                {
                    return new GenericResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
                }

                var todoItem = await _uow.TodoItem.GetById(command.Id);

                if (todoItem is null)
                {
                    return new GenericResponse("TodoItem not found", EOutputType.NotFound);
                }

                await _uow.TodoItem.Delete(todoItem);
                await _uow.Commit();

                return new GenericResponse("Deleted!");


            }
            catch (Exception ex)
            {
                return new GenericResponse(ex);
            }
        }

        public async Task<GenericResponse> Handle(TodoItemMarkAsDoneCommand command)
        {
            try
            {
                var validationCommand = command.Validate();

                if (validationCommand.IsValid == false)
                {
                    return new GenericResponse(string.Join("; ", validationCommand.Errors), EOutputType.BusinessValidation);
                }

                var todoItem = await _uow.TodoItem.GetById(command.Id);

                if (todoItem is null)
                {
                    return new GenericResponse("TodoItem not found", EOutputType.NotFound);
                }

                todoItem.MarkAsDone();

                await _uow.TodoItem.Update(todoItem);
                await _uow.Commit();

                return new GenericResponse("Mark as Done!");
            }
            catch (Exception ex)
            {
                return new GenericResponse(ex);
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
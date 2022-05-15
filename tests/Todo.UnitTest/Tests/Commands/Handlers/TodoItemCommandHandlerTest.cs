using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Handlers;
using Todo.Domain.Commands.UpdateCommands;
using Todo.Domain.Contracts.Commands.Handlers;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Contracts.Services;
using Todo.Domain.Enums;
using Todo.UnitTest.Mocks;
using Xunit;

namespace Todo.UnitTest.Tests.Commands.Handlers
{
    public class TodoItemCommandHandlerTest
    {
        private readonly ITodoItemRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMessageService _messageService;
        private readonly ILogger<TodoItemCommandHandler> _logger;
        private readonly ITodoItemCommandHandler _handler;
        private TodoItemCreateCommand createCommandNotInstantiated;
        private TodoItemDeleteCommand deleteCommandNotInstantiated;
        private TodoItemUpdateCommand updateCommandNotInstantiated;
        private TodoItemMarkAsDoneCommand markAsDoneCommandNotInstantiated;

        public TodoItemCommandHandlerTest()
        {
            _repository = new TodoItemRepositoryFake();
            _uow = new UnitOfWorkFake(_repository);
            _logger = new Logger<TodoItemCommandHandler>(new LoggerFactory());
            _messageService = new MessageServiceFake();
            _handler = new TodoItemCommandHandler(_uow, _messageService, _logger);

            createCommandNotInstantiated = null;
            deleteCommandNotInstantiated = null;
            updateCommandNotInstantiated = null;
            markAsDoneCommandNotInstantiated = null;
        }

        [Fact]
        public async Task When_Creating_Valid_TodoItem_Returns_Success()
        {
            // Arrange
            var command = new TodoItemCreateCommand()
            {
                Title = "Todo Xpto",
                Done = false
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.True(response.Success, response.Message);
        }

        [Fact]
        public Task When_Create_TodoItem_With_Not_Instance_CreateCommand_Returns_Unsuccessfully()
        {
            // Arrange            

            // Act            

            // Assert
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(createCommandNotInstantiated));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task When_Create_TodoItem_Empty_Returns_Unsuccessfully()
        {
            // Arrange
            var command = new TodoItemCreateCommand();

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_Create_TodoItem_With_Title_Less_Than_Five_Characters_Returns_Unsuccessfully()
        {
            // Arrange
            var command = new TodoItemCreateCommand()
            {
                Title = "Todo", // requires at least five characters
                Done = false
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_Create_TodoItem_With_Title_That_Already_Exists_Returns_Unsuccessfully()
        {
            // Arrange
            var command = new TodoItemCreateCommand()
            {
                Title = "First Task", // title already exists in repository Fake
                Done = false
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_Deleting_Valid_TodoItem_Returns_Success()
        {
            // Arrange
            var command = new TodoItemDeleteCommand()
            {
                Id = Guid.Parse("ddffe2dc-b162-406d-8520-429043bd9b7c")
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.Equal(EOutputType.Success, response.OutputType);
        }

        [Fact]
        public Task When_trying_to_delete_TodoItem_With_Not_Instance_DeleteCommand_Returns_Unsuccessfully()
        {
            // Arrange            

            // Act            

            // Assert            
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(deleteCommandNotInstantiated));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task When_trying_to_delete_todoItem_with_non_existent_ID_returns_unsuccessful()
        {
            // Arrange
            var command = new TodoItemDeleteCommand()
            {
                Id = Guid.Parse("6d8268a8-d6fe-4b79-90d0-f2ddb1231648")
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.NotFound, response.OutputType);
        }

        [Fact]
        public async Task When_trying_to_delete_any_emptyItem_it_returns_without_success()
        {
            // Arrange
            var command = new TodoItemDeleteCommand();

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_Updating_Valid_TodoItem_Returns_Success()
        {
            // Arrange
            var command = new TodoItemUpdateCommand()
            {
                Id = Guid.Parse("ddffe2dc-b162-406d-8520-429043bd9b7c"),
                Title = "new tilte",
                Done = false
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.Equal(EOutputType.Success, response.OutputType);
        }

        [Fact]
        public Task When_trying_to_update_TodoItem_With_Not_Instance_UpdateCommand_Returns_Unsuccessfully()
        {
            // Arrange            

            // Act            

            // Assert            
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(updateCommandNotInstantiated));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task When_trying_to_update_todoItem_with_non_existent_ID_returns_unsuccessful()
        {
            // Arrange
            var command = new TodoItemUpdateCommand()
            {
                Id = Guid.Parse("6d8268a8-d6fe-4b79-90d0-f2ddb1231648"),
                Title = "new title",
                Done = false
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.NotFound, response.OutputType);
        }

        [Fact]
        public async Task When_trying_to_update_any_emptyItem_it_returns_without_success()
        {
            // Arrange
            var command = new TodoItemUpdateCommand();

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_MarkAsDone_Valid_TodoItem_Returns_Success()
        {
            // Arrange
            var command = new TodoItemMarkAsDoneCommand()
            {
                Id = Guid.Parse("ddffe2dc-b162-406d-8520-429043bd9b7c")
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.Equal(EOutputType.Success, response.OutputType);
        }

        [Fact]
        public Task When_trying_to_MarkAdDone_TodoItem_With_Not_Instance_UpdateCommand_Returns_Unsuccessfully()
        {
            // Arrange            

            // Act            

            // Assert
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(markAsDoneCommandNotInstantiated));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task When_trying_to_MarkAdDone_todoItem_with_non_existent_ID_returns_unsuccessful()
        {
            // Arrange
            var command = new TodoItemMarkAsDoneCommand()
            {
                Id = Guid.Parse("6d8268a8-d6fe-4b79-90d0-f2ddb1231648")
            };

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.NotFound, response.OutputType);
        }

        [Fact]
        public async Task When_trying_to_MarkAsDone_any_emptyItem_it_returns_without_success()
        {
            // Arrange
            var command = new TodoItemMarkAsDoneCommand();

            // Act
            var response = await _handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_updating_all_tasks_to_Done()
        {
            // Arrange
            var isThereTaskNotDone = _repository.Query().Where(x => x.Done == false).Any();

            // Act
            await _handler.UpdateAllToDone();

            // Assert
            // Como testar um método sem saída/retorno? Verificando seus efeitos colaterais (side effects)
            // Primeiro verificamos se existe pelo menos uma task não concluída (Done == false)
            // Após a execução do Handler, verificamos que NÃO existe nenhuma task com status de não concluída (Done == true)
            Assert.True(isThereTaskNotDone, "Para um teste eficaz é necessário que exista pelo menos uma tarefa não concluída (Done == false) no repositório Fake");
            Assert.False(_repository.Query().Where(x => x.Done == false).Any(), "Todos os itens devem ser atualizados para o status de concluído (Done)");
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Commands.DeleteCommands;
using Todo.Domain.Commands.Handlers;
using Todo.Domain.Enums;
using Todo.UnitTest.Mocks;
using Xunit;

namespace Todo.UnitTest.Tests.Commands.Handlers
{
    public class TodoItemCommandHandlerTest
    {
        private readonly TodoItemRepositoryFake repository;
        private readonly UnitOfWorkFake uow;
        private readonly TodoItemCommandHandler handler;

        public TodoItemCommandHandlerTest()
        {
            repository = new TodoItemRepositoryFake();
            uow = new UnitOfWorkFake(repository);
            handler = new TodoItemCommandHandler(uow);
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
            var response = await handler.Handle(command);

            // Assert
            Assert.True(response.Success, response.Message);
        }

        [Fact]
        public async Task When_Create_TodoItem_Empty_Returns_Unsuccessfully()
        {
            // Arrange
            var command = new TodoItemCreateCommand();

            // Act
            var response = await handler.Handle(command);

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
            var response = await handler.Handle(command);

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
            var response = await handler.Handle(command);

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
                Id = 1
            };

            // Act
            var response = await handler.Handle(command);

            // Assert
            Assert.True(response.Success, response.Message);
            Assert.Equal(EOutputType.Success, response.OutputType);
        }

        [Fact]
        public async Task When_trying_to_delete_todoItem_with_non_existent_ID_returns_unsuccessful()
        {
            // Arrange
            var command = new TodoItemDeleteCommand()
            {
                Id = 99
            };

            // Act
            var response = await handler.Handle(command);

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
            var response = await handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }

        [Fact]
        public async Task When_updating_all_tasks_to_Done()
        {
            // Arrange
            var isThereTaskNotDone = repository.Query().Where(x => x.Done == false).Any();

            // Act
            await handler.UpdateAllToDone();

            // Assert
            // Como testar um método sem saída/retorno? Verificando seus efeitos colaterais (side effects)
            // Primeiro verificamos se existe pelo menos uma task não concluída (Done == false)
            // Após a execução do Handler, verificamos que NÃO existe nenhuma task com status de não concluída (Done == true)
            Assert.True(isThereTaskNotDone, "Para um teste eficaz é necessário que exista pelo menos uma tarefa não concluída (Done == false) no repositório Fake");
            Assert.False(repository.Query().Where(x => x.Done == false).Any(), "Todos os itens devem ser atualizados para o status de concluído (Done)");
        }
    }
}
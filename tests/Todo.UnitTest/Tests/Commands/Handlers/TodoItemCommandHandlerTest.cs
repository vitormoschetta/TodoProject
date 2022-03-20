using System.Threading.Tasks;
using Todo.Domain.Commands.CreateCommands;
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
        public async Task When_Create_TodoItem_With_Success()
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
        public async Task When_Create_TodoItem_Empty_With_Not_Success()
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
        public async Task When_Create_TodoItem_With_Title_Less_Than_Five_Characters_Not_Success()
        {
            // Arrange
            var command = new TodoItemCreateCommand()
            {
                Title = "Todo",
                Done = false
            };          

            // Act
            var response = await handler.Handle(command);

            // Assert
            Assert.False(response.Success, response.Message);
            Assert.Equal(EOutputType.BusinessValidation, response.OutputType);
        }
    }
}
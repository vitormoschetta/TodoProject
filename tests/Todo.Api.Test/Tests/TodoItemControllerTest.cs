using System.Net;
using System.Text;
using Newtonsoft.Json;
using Todo.Api.Test.Mocks;
using Todo.Application.Commands.Requests;
using Todo.Domain.Entities;

namespace Todo.Api.Test.Tests
{
    public class TodoItemControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        public TodoItemControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task GetAll_Endpoints_Return_Success(string endpoint)
        {
            // Arrange            

            // Act
            var response = await _httpClient.GetAsync(endpoint);
            var todoItems = JsonConvert.DeserializeObject<IList<TodoItem>>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(todoItems.Any());
        }


        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Get_Endpoints_ById_Return_Success(string endpoint)
        {
            // Arrange
            var id = Guid.Parse("ddffe2dc-b162-406d-8520-429043bd9b7c");

            // Act
            var response = await _httpClient.GetAsync($"{endpoint}/{id}");
            var todoItem = JsonConvert.DeserializeObject<TodoItem>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Task 01", todoItem.Title);
        }


        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Post_Endpoints_Return_Success(string endpoint)
        {
            // Arrange
            var todoItemCreateCommand = new CreateTodoItemRequest()
            {
                Title = "task 04",
                Done = false
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(todoItemCreateCommand), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Post_Endpoints_Empty_Title_Return_BadRequest(string endpoint)
        {
            // Arrange
            var todoItemCreateCommand = new CreateTodoItemRequest()
            {
                Title = "",
                Done = false
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(todoItemCreateCommand), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Put_Endpoints_Return_Success(string endpoint)
        {
            // Arrange           
            var todoItemUpdateCommand = new UpdateTodoItemRequest()
            {
                Id = Guid.Parse("0afe7382-2ac0-4ddf-a2bd-432da680b924"),
                Title = "Task 02 updated",
                Done = true
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(todoItemUpdateCommand), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{endpoint}/{todoItemUpdateCommand.Id}", content);
            var todoItemResponse = await _httpClient.GetAsync($"{endpoint}/{todoItemUpdateCommand.Id}");
            var todoItem = JsonConvert.DeserializeObject<TodoItem>(todoItemResponse.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Task 02 updated", todoItem.Title);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Put_Endpoints_TodoItem_Id_Different_Return_BadRequest(string endpoint)
        {
            // Arrange
            var id = Guid.Parse("10e45e3a-968d-48f0-b585-17dd81bb540b");

            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                Title = "Todo item new",
                Done = true
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{endpoint}/10e45e3a-968d-48f0-b585-17dd81bb540b", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Put_Endpoints_TodoItem_Id_Does_Not_Exist_Return_NotFound(string endpoint)
        {
            // Arrange
            var todoItem = new TodoItem()
            {
                Id = Guid.Parse("7b9e3cb4-5027-4f55-9771-16c1624d5f55"),
                Title = "Todo item new",
                Done = true
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{endpoint}/{todoItem.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Delete_Endpoints_Return_Success(string endpoint)
        {
            // Arrange
            var id = Guid.Parse("05670b31-6ae5-4251-be13-c6717449df3c");

            // Act
            var response = await _httpClient.DeleteAsync($"{endpoint}?id={id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
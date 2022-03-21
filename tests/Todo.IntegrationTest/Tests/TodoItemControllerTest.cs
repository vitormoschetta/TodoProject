using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Api;
using Todo.Domain.Commands.CreateCommands;
using Todo.Domain.Models;
using Todo.IntegrationTest.Mocks;
using Xunit;

namespace Todo.IntegrationTest.Tests
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
        public async Task Post_Endpoints_Return_Success(string url)
        {
            // Arrange
            var todoItem = new TodoItemCreateCommand()
            {
                Title = "Todo item",
                Done = false
            };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Post_Endpoints_Return_BadRequest(string url)
        {
            // Arrange
            var todoItem = new TodoItemCreateCommand() { Title = "", Done = false };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem")]
        public async Task Get_Endpoints_Return_Success(string url)
        {
            // Arrange
            var todoItem = new TodoItemCreateCommand() { Title = "Todo item", Done = false };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");
            _ = await _httpClient.PostAsync(url, content);

            // Act
            var response = await _httpClient.GetAsync(url);
            var todoItems = JsonConvert.DeserializeObject<IList<TodoItem>>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, todoItems.Count);
        }

        [Theory]
        [InlineData("/api/todoItem/100")]
        public async Task Get_Endpoints_With_Parameter_Return_Success(string url)
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/100")]
        public async Task Put_Endpoints_Return_Success(string url)
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.Parse("10e45e3a-968d-48f0-b585-17dd81bb540b"),
                Title = "Todo item new",
                Done = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/10e45e3a-968d-48f0-b585-17dd81bb540b")]
        public async Task Put_Endpoints_Return_BadRequest(string url)
        {
            // Arrange
            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                Title = "Todo item new",
                Done = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/7b9e3cb4-5027-4f55-9771-16c1624d5f55")]
        public async Task Put_Endpoints_Return_NotFound(string url)
        {
            // Arrange
            var todoItem = new TodoItem()
            {
                Id = Guid.Parse("7b9e3cb4-5027-4f55-9771-16c1624d5f55"),
                Title = "Todo item new",
                Done = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/100")]
        public async Task Delete_Endpoints_Return_Success(string url)
        {
            // Arrange

            // Act
            var response = await _httpClient.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todoItem/9999")]
        public async Task Delete_Endpoints_Return_NotFound(string url)
        {
            // Arrange

            // Act
            var response = await _httpClient.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
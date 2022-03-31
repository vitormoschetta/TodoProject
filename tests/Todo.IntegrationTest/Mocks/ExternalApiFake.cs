using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Contracts.Services.External;
using Todo.Domain.Models;

namespace Todo.IntegrationTest.Mocks
{
    public class ExternalApiFake : IExternalApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExternalApiFake(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<HttpStatusCode> DeleteTodoItem(TodoItem todoItem)
        {
            return await Task.FromResult(HttpStatusCode.OK);
        }

        public async Task<HttpStatusCode> PostTodoItem(TodoItem todoItem)
        {
            return await Task.FromResult(HttpStatusCode.OK);
        }

        public async Task<HttpStatusCode> PutTodoItem(TodoItem todoItem)
        {
            return await Task.FromResult(HttpStatusCode.OK);
        }
    }
}
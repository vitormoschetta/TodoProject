using System.Net;
using System.Threading.Tasks;
using Todo.Domain.Contracts.Services.External;
using Todo.Domain.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Todo.Infrastructure.Services
{
    public class ExternalApi : IExternalApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExternalApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ExternalApi:BaseAddress"]);
        }

        public async Task<HttpStatusCode> PostTodoItem(TodoItem todoItem)
        {
            string queryString = "TodoItem";

            StringContent requestContent = new StringContent(JsonSerializer.Serialize(todoItem), Encoding.UTF8, "application/json");

            using (HttpResponseMessage httpResponse = await _httpClient.PostAsync(queryString, requestContent))
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                }

                return HttpStatusCode.OK;
            }
        }

        public async Task<HttpStatusCode> PutTodoItem(TodoItem todoItem)
        {
            string queryString = "TodoItem";

            StringContent requestContent = new StringContent(JsonSerializer.Serialize(todoItem), Encoding.UTF8, "application/json");

            using (HttpResponseMessage httpResponse = await _httpClient.PutAsync(queryString, requestContent))
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                }

                return HttpStatusCode.OK;
            }
        }

        public async Task<HttpStatusCode> DeleteTodoItem(TodoItem todoItem)
        {
            string queryString = $"TodoItem?id={todoItem.Id}";

            StringContent requestContent = new StringContent(JsonSerializer.Serialize(todoItem), Encoding.UTF8, "application/json");

            using (HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(queryString))
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                }

                return HttpStatusCode.OK;
            }
        }
    }
}
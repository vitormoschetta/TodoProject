using System.Net;
using System.Text;
using Newtonsoft.Json;
using Todo.UI.Blazor.Models;

namespace Todo.UI.Blazor.Services;
public class TodoService : ITodoService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public TodoService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("API_URL_CONNECTION"));
    }

    public async Task<(HttpStatusCode, IEnumerable<TodoItem>)> GetAll()
    {
        string queryString = "TodoItem";

        using (HttpResponseMessage httpResponse = await _httpClient.GetAsync(queryString))
        {
            string content = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(content);

            return (httpResponse.StatusCode, response);
        }
    }

    public async Task<(HttpStatusCode, GenericResponse)> Add(TodoItem todo)
    {
        string queryString = "TodoItem";

        StringContent requestContent = new StringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");

        using (HttpResponseMessage httpResponse = await _httpClient.PostAsync(queryString, requestContent))
        {
            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<GenericResponse>(responseContent);

            return (httpResponse.StatusCode, response);
        }
    }

    public async Task<(HttpStatusCode, GenericResponse)> Delete(Guid id)
    {
        string queryString = $"TodoItem?id={id}";

        using (HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(queryString))
        {
            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<GenericResponse>(responseContent);

            return (httpResponse.StatusCode, response);
        }
    }

    public async Task<(HttpStatusCode, GenericResponse)> Update(TodoItem todo)
    {
        string queryString = $"TodoItem/{todo.Id}";

        StringContent requestContent = new StringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");

        using (HttpResponseMessage httpResponse = await _httpClient.PutAsync(queryString, requestContent))
        {
            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<GenericResponse>(responseContent);

            return (httpResponse.StatusCode, response);
        }
    }
}
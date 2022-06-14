using System.Text.Json.Serialization;
using Todo.Application.Enums;

namespace Todo.Application.Commands.Responses
{
    public class GenericResponse
    {
        public GenericResponse(string message, object data = null)
        {
            Message = message;
            Data = data;
            StatusResponse = EStatusResponse.Ok;
        }

        public GenericResponse(string message, EStatusResponse outputType)
        {
            Message = message;
            StatusResponse = outputType;
        }

        public GenericResponse(Exception ex)
        {
            StatusResponse = EStatusResponse.InternalServerError;
            Message = ex.Message;
        }

        [JsonIgnore]
        public EStatusResponse StatusResponse { get; set; }
        public bool Success => StatusResponse == EStatusResponse.Ok;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
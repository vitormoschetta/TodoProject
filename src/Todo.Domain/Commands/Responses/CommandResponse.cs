using System;
using System.Text.Json.Serialization;
using Todo.Domain.Enums;

namespace Todo.Domain.Commands.Responses
{
    public class CommandResponse
    {
        public CommandResponse(string message, object data = null)
        {
            Message = message;
            Data = data;
            OutputType = EOutputType.Success;
        }

        public CommandResponse(string message, EOutputType outputType)
        {
            Message = message;
            OutputType = outputType;
        }

        public CommandResponse(Exception ex)
        {
            OutputType = EOutputType.Failure;
            Message = ex.Message;
        }

        [JsonIgnore]
        public EOutputType OutputType { get; set; }
        public bool Success => OutputType == EOutputType.Success;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
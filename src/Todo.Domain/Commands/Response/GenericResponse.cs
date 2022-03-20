using System;
using Todo.Domain.Enums;

namespace Todo.Domain.Commands.Response
{
    public class GenericResponse
    {
        public GenericResponse() { }

        public GenericResponse(string message, object data = null)
        {
            Message = message;
            Data = data;
            OutputType = EOutputType.Success;
        }

        public GenericResponse(string message, EOutputType outputType)
        {
            Message = message;
            OutputType = outputType;
        }

        public GenericResponse(Exception ex)
        {
            OutputType = EOutputType.Failure;
            Message = ex.Message;
        }

        public EOutputType OutputType { get; set; }
        public bool Success => OutputType == EOutputType.Success;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
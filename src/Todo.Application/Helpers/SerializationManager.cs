using System.Text.Json;
using Todo.Application.Enums;

namespace Todo.Application.Helpers
{
    public class SerializationManager
    {
        public static string SerializeDomainEventToJson(EMessageType messageType, object data = null)
        {
            return JsonSerializer.Serialize(
                     new
                     {
                         Type = messageType.ToString(),
                         Data = data
                     });
        }
    }
}
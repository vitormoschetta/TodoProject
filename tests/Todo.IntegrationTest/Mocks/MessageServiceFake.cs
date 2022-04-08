using RabbitMQ.Client;
using Todo.Domain.Contracts.Services;

namespace Todo.IntegrationTest.Mocks
{
    public class MessageServiceFake : IMessageService
    {
        public void SendMessage(string message)
        {

        }
    }
}
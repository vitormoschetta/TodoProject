using RabbitMQ.Client;
using Todo.Domain.Contracts.Services;

namespace Todo.UnitTest.Mocks
{
    public class MessageServiceFake : IMessageService
    {
        public void SendMessage(string message)
        {

        }
    }
}
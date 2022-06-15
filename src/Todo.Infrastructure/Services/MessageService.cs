using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Todo.Application.Contracts.Services;

namespace Todo.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;
        private readonly string _queueName;

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:Host"] };
            _queueName = _configuration["RabbitMQ:Queue"];
        }


        public void SendMessage(string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(_queueName, false, false, false, null);

                var body = System.Text.Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
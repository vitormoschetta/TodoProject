namespace Todo.Domain.Contracts.Services
{
    public interface IMessageService
    {
        void SendMessage(string message);
    }
}
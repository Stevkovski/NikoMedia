namespace Services.Queueservice
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusessage, string queueName);
    }
}
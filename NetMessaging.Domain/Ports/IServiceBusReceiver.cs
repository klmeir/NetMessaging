namespace NetMessaging.Domain.Ports
{
    public interface IServiceBusReceiver
    {
        Task ReceiveMessageAsync(string authorName);
    }
}

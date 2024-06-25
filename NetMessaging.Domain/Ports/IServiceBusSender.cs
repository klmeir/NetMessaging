using NetMessaging.Domain.Entities;

namespace NetMessaging.Domain.Ports
{
    public interface IServiceBusSender
    {
        Task<bool> SendMessageAsync(Person person);
    }
}

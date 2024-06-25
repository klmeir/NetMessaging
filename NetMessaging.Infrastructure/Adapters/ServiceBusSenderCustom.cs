using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using NetMessaging.Domain.Entities;
using NetMessaging.Domain.Ports;
using System.Text.Json;

namespace NetMessaging.Infrastructure.Adapters
{
    [Repository]
    public class ServiceBusSenderCustom : IServiceBusSender
    {
        //private readonly IConfiguration _configuration;

        public ServiceBusSenderCustom(/*IConfiguration configuration*/)
        {
            //_configuration = configuration;
        }

        public async Task<bool> SendMessageAsync(Person personInput)
        {
            try
            {
                string connectionString = "Endpoint=sb://demopluxee.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DbSvtX2reQhyt8b/ulD8r+yZjGhEMI1UT+ASbGN801k=";//_configuration["AzureServiceBus:ConnectionString"];
                string queueName = "queue1";//_configuration["AzureServiceBus:QueueName"];

                // Create a ServiceBusClient object that you can use to create a ServiceBusSender
                await using (ServiceBusClient client = new ServiceBusClient(connectionString))
                {
                    // Create a sender for the queue
                    ServiceBusSender sender = client.CreateSender(queueName);

                    // Serialize personInput to JSON string
                    string jsonData = JsonSerializer.Serialize(personInput);

                    // Create a message that we can send. UTF-8 encoding is used when providing the message text.
                    ServiceBusMessage message = new ServiceBusMessage(jsonData);

                    // Send the message
                    await sender.SendMessageAsync(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to Azure Service Bus: {ex.Message}");
                return false;
            }
        }
    }
}

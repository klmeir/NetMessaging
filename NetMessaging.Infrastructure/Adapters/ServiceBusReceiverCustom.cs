using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using NetMessaging.Domain.Entities;
using NetMessaging.Domain.Ports;
using System.Text.Json;

namespace NetMessaging.Infrastructure.Adapters
{
    delegate void ProcessMessageHandlerDelegate(Person person);

    [Repository]
    public class ServiceBusReceiverCustom : IServiceBusReceiver
    {
        //private readonly IConfiguration _configuration;
        //private readonly IMessageProcessor _messageProcessor;

        public ServiceBusReceiverCustom(/*IConfiguration configuration, *//*IMessageProcessor messageProcessor*/)
        {
            //_configuration = configuration;
            //_messageProcessor = messageProcessor;
        }

        public async Task ReceiveMessageAsync(string authorName)
        {
            try
            {
                string connectionString = "Endpoint=sb://demopluxee.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DbSvtX2reQhyt8b/ulD8r+yZjGhEMI1UT+ASbGN801k=";//_configuration["AzureServiceBus:ConnectionString"];
                string queueName = "queue1";//_configuration["AzureServiceBus:QueueName"];

                // Create a ServiceBusClient object that you can use to create a ServiceBusProcessor
                await using (ServiceBusClient client = new ServiceBusClient(connectionString))
                {
                    // Create a processor that we can use to process the messages
                    ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

                    // Add handler to process messages
                    processor.ProcessMessageAsync += ProcessMessageHandler;

                    // Add handler to handle any errors
                    processor.ProcessErrorAsync += ErrorHandler;

                    // Start processing
                    await processor.StartProcessingAsync();

                    Console.WriteLine("Press any key to stop receiving messages...");
                    Console.ReadKey();

                    // Stop processing
                    Console.WriteLine("\nStopping the receiver...");
                    await processor.StopProcessingAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving messages from Azure Service Bus: {ex.Message}");
            }
        }

        private async Task ProcessMessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                // Process message content
                string jsonMessage = args.Message.Body.ToString();
                var personInput = JsonSerializer.Deserialize<Person>(jsonMessage);

                // Call message processor
                //await _messageProcessor.ProcessMessageAsync(personInput);

                // Complete the message to remove it from the queue
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                // Abandon the message on any processing error
                await args.AbandonMessageAsync(args.Message);
                // Optionally log or handle the error
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // Log or handle any errors from the Service Bus processor
            Console.WriteLine($"Error processing message: {args.Exception.Message}");

            return Task.CompletedTask;
        }
    }
}

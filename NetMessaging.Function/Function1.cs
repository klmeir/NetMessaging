using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NetMessaging.Domain.Entities;
using NetMessaging.Domain.Exception;
using NetMessaging.Domain.Ports;
using NetMessaging.Domain.Services;
using System.Text.Json;

namespace NetMessaging.Function
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IServiceBusReceiver _serviceBusReceiver;
        private readonly PersonService _service;

        public Function1(ILogger<Function1> logger, IServiceBusReceiver serviceBusReceiver, PersonService personService)
        {
            _logger = logger;
            _serviceBusReceiver = serviceBusReceiver;
            _service = personService;
        }

        [Function(nameof(Function1))]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {

            try
            {
                string connectionString = "Endpoint=sb://demopluxee.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DbSvtX2reQhyt8b/ulD8r+yZjGhEMI1UT+ASbGN801k=";
                string queueName = "queue1";

                // Create a ServiceBusClient object that you can use to create a ServiceBusProcessor
                await using (ServiceBusClient client = new ServiceBusClient(connectionString))
                {
                    // Create a processor that we can use to process the messages
                    ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions() { AutoCompleteMessages = false });

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

                Console.WriteLine(jsonMessage);

                // Call message processor
                //await _messageProcessor.ProcessMessageAsync(personInput);
                if ("CarlosMeriño".Equals(personInput.AuthorName))
                {
                    await _service.SaveToXml(personInput);

                    // Complete the message to remove it from the queue
                    await args.CompleteMessageAsync(args.Message);                    
                }
            }
            catch (Exception ex)
            {
                // Abandon the message on any processing error
                await args.AbandonMessageAsync(args.Message);
                // Optionally log or handle the error
                //Console.WriteLine($"Error processing message: {ex.Message}");

                _logger.LogError("An error occurred while storing the message in the database.");
                _logger.LogError(args.Message.Body.ToString());
                new CoreBusinessException("An error occurred while storing the message in the database.");
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

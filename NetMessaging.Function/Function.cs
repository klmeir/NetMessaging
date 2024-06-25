//using Azure.Messaging.ServiceBus;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.Logging;
//using NetMessaging.Domain.Entities;
//using NetMessaging.Domain.Exception;
//using NetMessaging.Domain.Services;
//using System.Text.Json;

//namespace NetMessaging.Function
//{
//    public class Function
//    {
//        private readonly ILogger<Function> _logger;
//        private readonly PersonService _service;

//        public Function(ILogger<Function> logger, PersonService personService)
//        {
//            _logger = logger;
//            _service = personService;
//        }

//        [Function(nameof(Function))]
//        public async Task Run(
//            [ServiceBusTrigger("queue1", Connection = "ConnectionString")]
//            ServiceBusReceivedMessage message,
//            ServiceBusMessageActions messageActions)
//        {

//            string jsonMessage = message.Body.ToString();
//            var personInput = JsonSerializer.Deserialize<Person>(jsonMessage);            
            
//            if ("CarlosMeriño".Equals(personInput.AuthorName))
//            {

//                try
//                {
//                    await _service.SaveToXml(personInput);

//                    // Complete the message
//                    await messageActions.CompleteMessageAsync(message);                    
//                }
//                catch (CoreBusinessException ex) {
//                    _logger.LogError(ex.Message);
//                    _logger.LogError(message.ToString());
//                    await messageActions.AbandonMessageAsync(message);
//                }               
//            }
//        }
//    }
//}

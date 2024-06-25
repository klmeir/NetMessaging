using NetMessaging.Domain.Entities;
using NetMessaging.Domain.Exception;
using NetMessaging.Domain.Ports;
using System.Xml.Serialization;

namespace NetMessaging.Domain.Services
{
    [DomainService]
    public class PersonService
    {
        private readonly IServiceBusSender _serviceBusSender;
        private readonly IPaymentResponseRepository _paymentResponseRepository;
        private const string AUTHOR_NAME = "CarlosMeriño";

        public PersonService(IServiceBusSender serviceBusSender, IPaymentResponseRepository paymentResponseRepository)
        {
            _serviceBusSender = serviceBusSender;
            _paymentResponseRepository = paymentResponseRepository;
        }        

        public async Task SaveToXml(Person person) 
        {
            try
            {

                // Crear el objeto XmlSerializer para la clase Persona
                XmlSerializer serializer = new XmlSerializer(typeof(Person));

                // Crear un StringWriter para almacenar el XML
                StringWriter stringWriter = new StringWriter();

                // Serializar el objeto Persona a XML
                serializer.Serialize(stringWriter, person);

                // Obtener el XML como una cadena
                string xmlPersona = stringWriter.ToString();

                await _paymentResponseRepository.SaveXml(new PaymentResponse { MessageXml = xmlPersona });
            }
            catch (System.Exception) {
                throw new CoreBusinessException("An error occurred while storing the message in the database.");
            }
        }

        public async Task<bool> SendMsg(Person person)
        {
            person.ProcessDate = DateTime.UtcNow;
            person.AuthorName = AUTHOR_NAME;
            person.InsurancePayment = new InsurancePayment
            {
                PaymentId = 1,
                Currency = "USD",
                Amount = 1000,
                PaymentDatetime = DateTime.UtcNow,
                Franchise = "Demo"
            };

            var result = await _serviceBusSender.SendMessageAsync(person);

            return result;
        }
    }
}

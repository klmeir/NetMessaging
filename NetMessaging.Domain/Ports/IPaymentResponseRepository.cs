using NetMessaging.Domain.Entities;

namespace NetMessaging.Domain.Ports
{
    public interface IPaymentResponseRepository
    {
        Task<PaymentResponse> SaveXml(PaymentResponse paymentResponse);
    }
}

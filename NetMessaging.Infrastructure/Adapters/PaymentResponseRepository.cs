using NetMessaging.Domain.Entities;
using NetMessaging.Domain.Ports;
using NetMessaging.Infrastructure.Ports;

namespace NetMessaging.Infrastructure.Adapters
{
    [Repository]
    public class PaymentResponseRepository : IPaymentResponseRepository
    {
        readonly IRepository<PaymentResponse> _dataSource;
        public PaymentResponseRepository(IRepository<PaymentResponse> dataSource) => _dataSource = dataSource
        ?? throw new ArgumentNullException(nameof(dataSource));

        public async Task<PaymentResponse> SaveXml(PaymentResponse p)
        {            
            return await _dataSource.AddAsync(p);
        }
    }
}

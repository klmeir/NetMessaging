using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NetMessaging.Domain.Entities;

namespace NetMessaging.Infrastructure.DataSource.ModelConfig
{
    public class PaymentResponseConfig : IEntityTypeConfiguration<PaymentResponse>
    {        
        public void Configure(EntityTypeBuilder<PaymentResponse> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);

            builder.Property(b => b.MessageXml).IsRequired();
        }
    }
}

using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class PaymentConfig : BaseConfig<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.Property(p => p.Amount)
               .HasColumnType("decimal(10,2)");
        builder.Property(p => p.PaymentMetod)
               .HasDefaultValue(PaymentMetod.COD.ToString());


        base.Configure(builder);
    }
}

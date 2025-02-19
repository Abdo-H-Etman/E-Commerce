using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class ShippingConfig : BaseConfig<Shipping>
{
    public override void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.ToTable("Shippings");

        builder.Property(p => p.ShippingStatus)
               .HasDefaultValue(ShippingStatus.Pending.ToString());
        builder.HasOne(p => p.Order)
               .WithOne(o => o.Shipping)
               .HasForeignKey<Shipping>(s => s.OrderId);

        base.Configure(builder);
    }
}

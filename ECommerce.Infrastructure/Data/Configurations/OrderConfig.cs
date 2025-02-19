using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class OrderConfig : BaseConfig<Order>
{
       public override void Configure(EntityTypeBuilder<Order> builder)
       {
              builder.ToTable("Orders");

              builder.Property(o => o.OrderStatus)
                     .HasDefaultValue(OrderStatus.Pending.ToString());
              builder.Property(o => o.TotalAmount)
                     .HasColumnType("decimal(10,2)")
                     .IsRequired();

              builder.HasOne(o => o.Payment)
                     .WithOne(p => p.Order)
                     .HasForeignKey<Payment>(p => p.OrderId);
              builder.HasOne(o => o.Shipping)
                     .WithOne()
                     .HasForeignKey<Shipping>(s => s.OrderId);
              builder.HasMany(o => o.OrderItems)
                     .WithOne(oi => oi.Order)
                     .HasForeignKey(oi => oi.OrderId);

              base.Configure(builder);
       }
}

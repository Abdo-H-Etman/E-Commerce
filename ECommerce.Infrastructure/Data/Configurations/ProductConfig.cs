using System;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class ProductConfig : BaseConfig<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Name)
               .HasMaxLength(150)
               .IsRequired();
        builder.Property(p => p.Description)
               .HasMaxLength(1000)
               .IsRequired();
        builder.Property(p => p.Price)
               .HasColumnType("decimal(10,2)")
               .IsRequired();
        builder.Property(p => p.Stock)
               .IsRequired();


       //  builder.HasOne(p => p.ProvidedBy)
       //         .WithMany()
       //         .HasForeignKey(p => p.UserId);
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);
        builder.HasMany(p => p.OrderItems)
               .WithOne(o => o.Product)
               .HasForeignKey(o => o.ProductId);
        builder.HasMany(p => p.Reviews)
               .WithOne(r => r.Product)
               .HasForeignKey(r => r.ProductId);
                      

        base.Configure(builder);
    }
}

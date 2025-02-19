using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class CarrierConfig : BaseConfig<Carrier>
{
    public override void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.ToTable("Carriers");

        builder.Property(c => c.Name)
               .HasMaxLength(100);
        builder.HasIndex(c => c.Email)
               .IsUnique();
        base.Configure(builder);
    }
}
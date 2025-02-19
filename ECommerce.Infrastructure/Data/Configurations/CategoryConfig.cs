using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class CategoryConfig : BaseConfig<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.Property(c => c.Name)
               .HasMaxLength(50)
               .IsRequired();
        builder.Property(c => c.Description)
               .HasMaxLength(500)
               .IsRequired();
                      
        base.Configure(builder);
    }
}

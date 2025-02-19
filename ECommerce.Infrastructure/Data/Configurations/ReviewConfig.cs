using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class ReviewConfig : BaseConfig<Review>
{
    public override void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        
        builder.Property(r => r.Comment)
               .HasMaxLength(1000);

        builder.HasOne(r => r.User)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}

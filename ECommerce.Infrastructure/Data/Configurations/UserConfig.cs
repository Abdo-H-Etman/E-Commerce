
// using ECommerce.Domain.Enums;
// using ECommerce.Domain.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace ECommerce.Infrastructure.Data.Configurations;

// public class UserConfig : BaseConfig<User>
// {
//     public override void Configure(EntityTypeBuilder<User> builder)
//     {
//         builder.ToTable("Users");

//         builder.HasIndex(u => u.Email)
//                .IsUnique();
//         builder.HasIndex("FirstName","LastName");       

//         builder.Property(u => u.FirstName)
//                .HasMaxLength(50)
//                .IsRequired();
//         builder.Property(u => u.LastName)
//                .HasMaxLength(50)
//                .IsRequired();
//         builder.Property(u => u.Email)
//                .IsRequired();
//         builder.Property(u => u.Password)
//                .IsRequired();
//         builder.Property(u => u.Phone); 
//         builder.Property(u => u.Role)
//                .HasDefaultValue(UserRole.Buyer.ToString());
                           

//         builder.HasMany(u => u.Orders)
//                .WithOne(o => o.User)
//                .HasForeignKey(o => o.UserId);
//         builder.HasMany(u => u.Reviews)
//                .WithOne(r => r.User)
//                .HasForeignKey(r => r.UserId);       

//         base.Configure(builder);
//     }
// }

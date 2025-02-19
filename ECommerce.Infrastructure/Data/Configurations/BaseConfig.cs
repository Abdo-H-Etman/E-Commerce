using ECommerce.Domain.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class BaseConfig<BaseModel> : IEntityTypeConfiguration<BaseModel>
    where BaseModel: IdModel
{
    public virtual void Configure(EntityTypeBuilder<BaseModel> builder)
    {
        builder.HasKey(b => b.Id);
    }
}

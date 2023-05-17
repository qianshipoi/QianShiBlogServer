using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BlogMetaConfiguration : IEntityTypeConfiguration<BlogMeta>
{
    public void Configure(EntityTypeBuilder<BlogMeta> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(t => t.Type);

        builder.Property(t => t.Name)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(t => t.Order)
            .HasDefaultValue(100)
            .IsRequired();
        builder.Property(t => t.Description).HasMaxLength(255);

        builder.Property(t => t.Type).HasMaxLength(16).IsRequired();

        builder.Property(t => t.Count).IsRequired();

        builder.Property(t => t.Parent).HasDefaultValue(0).IsRequired();

        builder.Property(t => t.CreatedBy).IsRequired();
        builder.Property(t => t.Created).IsRequired();
    }
}

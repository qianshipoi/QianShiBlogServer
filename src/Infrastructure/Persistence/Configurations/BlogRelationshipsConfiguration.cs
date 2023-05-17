using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BlogRelationshipsConfiguration : IEntityTypeConfiguration<BlogRelationships>
{
    public void Configure(EntityTypeBuilder<BlogRelationships> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Cid)
            .IsRequired();

        builder.Property(t => t.Mid)
            .IsRequired();
    }
}
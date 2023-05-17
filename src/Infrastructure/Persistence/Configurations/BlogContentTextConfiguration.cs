using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BlogContentTextConfiguration : IEntityTypeConfiguration<BlogContentText>
{
    public void Configure(EntityTypeBuilder<BlogContentText> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Text)
            .IsRequired();
    }
}

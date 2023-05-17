using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BlogContentConfiguration : IEntityTypeConfiguration<BlogContent>
{
    public void Configure(EntityTypeBuilder<BlogContent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Subtitle).HasMaxLength(255);

        builder.Property(t => t.Order).IsRequired();

        builder.Property(t => t.Status)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(t => t.Type)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(t => t.AllowComment).IsRequired();

        builder.Property(t => t.CommentCount).IsRequired();

        builder.Property(t => t.CreatedBy).IsRequired();

        builder.Property(t => t.Created).IsRequired();
    }
}

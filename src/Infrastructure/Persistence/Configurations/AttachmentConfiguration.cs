using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(t => t.Path)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.ContentType)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(t => t.Size)
            .IsRequired();

        builder.Property(t => t.Status)
            .IsRequired();
    }
}
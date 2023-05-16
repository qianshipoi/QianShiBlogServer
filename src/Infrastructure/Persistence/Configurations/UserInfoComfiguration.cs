using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserInfoComfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasIndex(t => t.Email);
        builder.Property(t => t.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.Salt)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(t => t.NikeName)
            .HasMaxLength(32);

        builder.Property(t => t.CreatedTime)
            .IsRequired();
    }
}

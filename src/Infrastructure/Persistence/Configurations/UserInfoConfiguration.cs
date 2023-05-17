using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(t => t.Email);
        builder.Property(t => t.Email)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(t => t.PasswordHash)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(t => t.Salt)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(t => t.NikeName)
            .HasMaxLength(32);

        builder.Property(t => t.Avatar)
            .HasMaxLength(255);

        builder.Property(t => t.Created)
            .IsRequired();
    }
}

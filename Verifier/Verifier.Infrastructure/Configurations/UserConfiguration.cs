using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Verifier.Domain.Users;

namespace Verifier.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "dbo");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FullName)
                .IsRequired();
        builder.Property(u => u.PhoneNumber)
                .IsRequired();
        builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetUtcDate()");
    }
}
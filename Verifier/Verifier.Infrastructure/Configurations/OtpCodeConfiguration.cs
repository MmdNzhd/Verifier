using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Verifier.Domain.Users;

namespace Verifier.Infrastructure.Configurations;

public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable("OtpCodes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.ExpireAt).IsRequired();
        builder.Property(x => x.IsUsed).IsRequired();
    }
}

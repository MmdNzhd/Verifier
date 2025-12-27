using Microsoft.EntityFrameworkCore;
using Verifier.Domain.Users;
using Verifier.Infrastructure.Configurations;

namespace Verifier.Infrastructure.Persistence;

public class VerifierDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();

    public VerifierDbContext(DbContextOptions<VerifierDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OtpCodeConfiguration());
    }
}

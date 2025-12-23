using Microsoft.EntityFrameworkCore;
using Verifier.Domain.Users;

namespace Verifier.Infrastructure.Persistence;

public class VerifierDbContext : DbContext
{
public DbSet<User> Users=> Set<User>();

    public VerifierDbContext(DbContextOptions<VerifierDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
    }
}

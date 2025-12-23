using Verifier.Application.Users;
using Verifier.Domain.Users;
using Verifier.Infrastructure.Persistence;

namespace Verifier.Infrastructure.Users;

public class EfUserRepository : IUserRepository
{
    private readonly VerifierDbContext _context;

    public EfUserRepository(VerifierDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        
    }

    public IReadOnlyList<User> GetAll()
    {
        return _context.Users.ToList();
    }
}

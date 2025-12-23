using Verifier.Application.Users;
using Verifier.Domain.Users;

namespace Verifier.Infrastructure.Users;

public class InMemoryUserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public IReadOnlyList<User> GetAll()
    {
        return _users;
    }

    public User? GetById(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
}

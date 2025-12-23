using Verifier.Domain.Users;

namespace Verifier.Application.Users;

public interface IUserRepository
{
    void Add(User user);
    IReadOnlyList<User> GetAll();
    User? GetById(Guid id);
}

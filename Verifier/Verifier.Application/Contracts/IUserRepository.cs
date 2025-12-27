using Verifier.Domain.Users;

namespace Verifier.Application.Contracts;

public interface IUserRepository
{
    Task<User?> GetByPhoneAsync(string phoneNumber);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}

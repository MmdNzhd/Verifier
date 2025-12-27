using Verifier.Application.Contracts;
using Verifier.Domain.Users;
using Verifier.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository
{
    private readonly VerifierDbContext _context;

    public UserRepository(VerifierDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByPhoneAsync(string phoneNumber) =>
        _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

    public Task<User?> GetByIdAsync(Guid id) =>
        _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}

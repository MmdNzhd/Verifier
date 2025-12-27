using Microsoft.EntityFrameworkCore;
using Verifier.Application.Contracts;
using Verifier.Domain.Users;
using Verifier.Infrastructure.Persistence;

namespace Verifier.Infrastructure.Users;

public class OtpRepository : IOtpRepository
{
    private readonly VerifierDbContext _context;

    public OtpRepository(VerifierDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OtpCode otp)
    {
        _context.OtpCodes.Add(otp);
        await _context.SaveChangesAsync();
    }

    public Task<OtpCode?> GetValidAsync(string phoneNumber, string code)
    {
        return _context.OtpCodes
            .OrderByDescending(x => x.ExpireAt)
            .FirstOrDefaultAsync(x =>
                x.PhoneNumber == phoneNumber &&
                x.Code == code &&
                !x.IsUsed &&
                x.ExpireAt > DateTime.UtcNow);
    }

    public async Task UpdateAsync(OtpCode otp)
    {
        _context.OtpCodes.Update(otp);
        await _context.SaveChangesAsync();
    }
}

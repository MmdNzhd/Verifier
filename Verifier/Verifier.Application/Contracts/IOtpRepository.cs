using Verifier.Domain.Users;

namespace Verifier.Application.Contracts;

public interface IOtpRepository
{
    Task AddAsync(OtpCode otp);
    Task<OtpCode?> GetValidAsync(string phoneNumber, string code);
    Task UpdateAsync(OtpCode otp);
}

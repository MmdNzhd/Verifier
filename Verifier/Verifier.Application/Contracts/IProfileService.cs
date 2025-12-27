using Verifier.Application.Dtos;

namespace Verifier.Application.Contracts;

public interface IProfileService
{
    Task<ProfileResponse> GetProfileAsync(Guid userId);
    Task UpdateProfileAsync(Guid userId, UpdateProfileRequest request);
    Task SendOtpForPhoneChangeAsync(Guid userId, string phoneNumber);
}
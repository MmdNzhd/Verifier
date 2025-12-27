using Verifier.Application.Contracts;
using Verifier.Application.Dtos;
using Verifier.Domain.Users;

namespace Verifier.Application.Users;

public class ProfileService : IProfileService
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpRepository _otpRepository;

    public ProfileService(IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _userRepository = userRepository;
        _otpRepository = otpRepository;
    }

    public async Task<ProfileResponse> GetProfileAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new ProfileResponse(user.FullName, user.PhoneNumber);
    }

    public async Task UpdateProfileAsync(Guid userId, UpdateProfileRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        bool phoneChanged = (user.PhoneNumber != request.PhoneNumber);

        if (phoneChanged)
        {
            if (string.IsNullOrEmpty(request.Code))
            {
                throw new Exception("Code is required when changing phone number.");
            }

            // Verify OTP for new phone
            var otp = await _otpRepository.GetValidAsync(request.PhoneNumber, request.Code);
            if (otp == null)
            {
                throw new Exception("Invalid or expired code.");
            }
            otp.Use();
            await _otpRepository.UpdateAsync(otp);
        }

        user.UpdateProfile(request.FullName, request.PhoneNumber);
        await _userRepository.UpdateAsync(user);
    }

    public async Task SendOtpForPhoneChangeAsync(Guid userId, string phoneNumber)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Check if phone is already taken
        var existingUser = await _userRepository.GetByPhoneAsync(phoneNumber);
        if (existingUser != null && existingUser.Id != userId)
        {
            throw new Exception("Phone number already in use.");
        }

        var code = Random.Shared.Next(100000, 999999).ToString();
        var otp = new OtpCode(phoneNumber, code);

        await _otpRepository.AddAsync(otp);
        Console.WriteLine($"OTP Code for {phoneNumber}: {code}"); // For testing
        // Note: In real app, send SMS here
    }
}
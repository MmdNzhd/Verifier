using Verifier.Application.Contracts;
using Verifier.Domain.Users;

namespace Verifier.Application.Users;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IOtpRepository _otpRepo;
    private readonly ISmsService _sms;
    private readonly IJwtService _jwt;

    public AuthService(
        IUserRepository userRepo,
        IOtpRepository otpRepo,
        ISmsService sms,
        IJwtService jwt)
    {
        _userRepo = userRepo;
        _otpRepo = otpRepo;
        _sms = sms;
        _jwt = jwt;
    }

    public async Task SendOtpAsync(string phoneNumber)
    {
        var code = Random.Shared.Next(100000, 999999).ToString();
        var otp = new OtpCode(phoneNumber, code);

        await _otpRepo.AddAsync(otp);
        await _sms.SendAsync(phoneNumber, $"کد ورود: {code}");
    }

    public async Task<string> VerifyOtpAsync(string phoneNumber, string code)
    {
        var otp = await _otpRepo.GetValidAsync(phoneNumber, code);
        if (otp == null)
            throw new Exception("کد نامعتبر یا منقضی شده");

        otp.Use();
        await _otpRepo.UpdateAsync(otp);

        var user = await _userRepo.GetByPhoneAsync(phoneNumber);
        if (user == null)
        {
            user = new User(phoneNumber);
            await _userRepo.AddAsync(user);
        }

        return _jwt.Generate(user);
    }
}

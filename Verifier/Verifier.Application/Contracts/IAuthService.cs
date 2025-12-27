namespace Verifier.Application.Contracts;

public interface IAuthService
{
    Task SendOtpAsync(string phoneNumber);
    Task<string> VerifyOtpAsync(string phoneNumber, string code);
}

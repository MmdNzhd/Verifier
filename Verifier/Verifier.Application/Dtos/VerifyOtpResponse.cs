namespace Verifier.Application.Dtos;

public class VerifyOtpResponse
{
    public string Token { get; set; } = null!;

    public VerifyOtpResponse(string token)
    {
        Token = token;
    }
}

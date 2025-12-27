using Microsoft.AspNetCore.Mvc;
using Verifier.Application.Contracts;
using Verifier.Application.Dtos;

namespace Verifier.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp(SendOtpRequest request)
    {
        await _authService.SendOtpAsync(request.PhoneNumber);
        return Ok();
    }

    [HttpPost("verify-otp")]
    public async Task<ActionResult<VerifyOtpResponse>> VerifyOtp(VerifyOtpRequest request)
    {
        var token = await _authService.VerifyOtpAsync(
            request.PhoneNumber,
            request.Code);

        return Ok(new VerifyOtpResponse(token));
    }
}

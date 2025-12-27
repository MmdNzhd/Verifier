namespace Verifier.Application.Dtos;

public record UpdateProfileRequest(string FullName, string PhoneNumber, string? Code = null);
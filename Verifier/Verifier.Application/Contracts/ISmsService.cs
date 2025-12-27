namespace Verifier.Application.Contracts;

public interface ISmsService
{
    Task SendAsync(string phoneNumber, string message);
}

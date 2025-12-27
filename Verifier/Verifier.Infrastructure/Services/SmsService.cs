using Verifier.Application.Contracts;

namespace Verifier.Infrastructure.Services;

public class SmsService : ISmsService
{
    public Task SendAsync(string phoneNumber, string message)
    {
        // Placeholder SMS sender: in real deployments integrate with an SMS gateway.
        Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
        return Task.CompletedTask;
    }
}

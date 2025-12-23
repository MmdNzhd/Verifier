namespace Verifier.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string? FullName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { } // 👈 فقط برای EF

    public User(string firstName, string lastName, string phoneNumber)
    {
        Id = Guid.NewGuid();
        FullName = $"{firstName} {lastName}";
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
    }
}

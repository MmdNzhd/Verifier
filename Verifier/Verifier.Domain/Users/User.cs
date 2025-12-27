namespace Verifier.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private User() { } // EF

    
    public User(string phoneNumber)
    {
        Id = Guid.NewGuid();
        PhoneNumber = phoneNumber;
        FullName = "کاربر جدید";
        CreatedAt = DateTime.UtcNow;
    }

 
    public User(string firstName, string lastName, string phoneNumber)
        : this(phoneNumber)
    {
        FullName = $"{firstName} {lastName}";
    }

    public void UpdateProfile(string fullName, string phoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }
}

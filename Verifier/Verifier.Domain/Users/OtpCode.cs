namespace Verifier.Domain.Users;

public class OtpCode
{
    public Guid Id { get; private set; }
    public string PhoneNumber { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public DateTime ExpireAt { get; private set; }
    public bool IsUsed { get; private set; }

    private OtpCode() { } // EF

    public OtpCode(string phoneNumber, string code)
    {
        Id = Guid.NewGuid();
        PhoneNumber = phoneNumber;
        Code = code;
        ExpireAt = DateTime.UtcNow.AddMinutes(2);
        IsUsed = false;
    }

    public void Use()
    {
        IsUsed = true;
    }
}

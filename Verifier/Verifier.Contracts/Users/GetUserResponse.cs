namespace Verifier.Contracts.Users;

public class GetUserResponse
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}

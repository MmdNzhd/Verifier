namespace Verifier.Application.Dtos;

public class CreateUserRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime BirthDate { get; set; }
}
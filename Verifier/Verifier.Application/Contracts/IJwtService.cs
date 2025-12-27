using Verifier.Domain.Users;

namespace Verifier.Application.Contracts;

public interface IJwtService
{
    string Generate(User user);
}

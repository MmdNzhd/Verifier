using Verifier.Domain.Users;

namespace Verifier.Application.Users;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

  public User Create(string firstName, string lastName, string phoneNumber)
{
    var user = new User(firstName, lastName, phoneNumber);
    _repository.Add(user);
    return user;
}


    public IReadOnlyList<User> GetAll()
    {
        return _repository.GetAll();
    }

    public User? GetById(Guid id)
    {
        return _repository.GetById(id);
    }
}

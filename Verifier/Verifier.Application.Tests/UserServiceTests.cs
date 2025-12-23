using Moq;
using Verifier.Application.Users;
using Verifier.Domain.Users;

namespace Verifier.Application.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepository.Object);
    }

    [Fact]
    public void GetById_ValidId_ShouldReturnUser()
    {
        var expectedUser = new User("John", "Doe", "09123456789");
        var userId = expectedUser.Id;
        
        _mockRepository.Setup(r => r.GetById(userId))
            .Returns(expectedUser);

        var result = _userService.GetById(userId);

        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result.Id);
        Assert.Equal(expectedUser.FullName, result.FullName);
        Assert.Equal("09123456789", result.PhoneNumber);
        _mockRepository.Verify(r => r.GetById(userId), Times.Once);
    }

    [Fact]
    public void GetById_InvalidId_ShouldReturnNull()
    {
        var invalidId = Guid.NewGuid();
        
        _mockRepository.Setup(r => r.GetById(invalidId))
            .Returns((User?)null);

        var result = _userService.GetById(invalidId);

        Assert.Null(result);
        _mockRepository.Verify(r => r.GetById(invalidId), Times.Once);
    }
}

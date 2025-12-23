using Microsoft.AspNetCore.Mvc;
using Verifier.Application.Users;
using Verifier.Contracts.Users;

namespace Verifier.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

[HttpPost]
public IActionResult Create(CreateUserRequest request)
{
    var user = _userService.Create(
        request.FirstName,
        request.LastName,
        request.PhoneNumber
    );

    return Ok(user);
}


    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }
}

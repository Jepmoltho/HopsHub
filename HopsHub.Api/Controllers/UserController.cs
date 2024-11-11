using HopsHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
	{
        _userService = userService;
	}

    [EnableRateLimiting("NormalMaxRequestPolicy")]
    [HttpGet("/Users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetUsers();

        if (!result.Any())
        {
            return NotFound("No users found in the database");
        }

        return Ok(result);
    }
}


using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.DTOs;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
	private readonly IAccountService _accountService;

	public LoginController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpPost("/Login")]
	public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
	{
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

		var result = await _accountService.LoginAsync(loginDTO);

		if (!result.Succeeded)
		{
			return Unauthorized(result.Message);
		}
		
		return Ok(result.Message);
	}

	[HttpPost("/Logout")]
	public async Task<IActionResult> Logout()
	{
		var result = await _accountService.LogoutAsync();

		if (!result.Succeeded)
		{
			return BadRequest();
		}

		return Ok(result.Message);
	}

	//Todo: Create User
	[HttpPost("/CreateUser")]
	public async Task<IActionResult> CreateUser([FromBody] LoginDTO loginDTO)
	{
		var result = await _accountService.CreateUser(loginDTO);

		if (!result.Succeeded)
		{
			return BadRequest(result.Message);
		}

		return Ok(result.Message);
	}

	//Todo: Delete user

	//Todo: Forgot password

	//Todo: Change password
}


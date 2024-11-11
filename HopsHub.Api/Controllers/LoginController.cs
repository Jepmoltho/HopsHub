using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.DTOs;
using Microsoft.AspNetCore.RateLimiting;

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

    [EnableRateLimiting("NormalMaxRequestPolicy")]
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

    [EnableRateLimiting("NormalMaxRequestPolicy")]
    [HttpPost("/Logout")]
	public async Task<IActionResult> Logout()
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var result = await _accountService.LogoutAsync();

		if (!result.Succeeded)
		{
			return BadRequest();
		}

		return Ok(result.Message);
	}

    [EnableRateLimiting("HardMaxRequestPolicy")]
    [HttpPost("/CreateUser")]
	public async Task<IActionResult> CreateUser([FromBody] LoginDTO loginDTO)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var result = await _accountService.CreateUser(loginDTO);

		if (!result.Succeeded)
		{
			return BadRequest(result.Message);
		}

		return Ok(result.Message);
	}

	[EnableRateLimiting("HardMaxRequestPolicy")]
	[HttpDelete("/DeleteUser")]
	public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDTO deleteUserDTO)
	{
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _accountService.DeleteUser(deleteUserDTO);

        if (!result.Succeeded)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

	//Todo: Confirm email

    //Todo: Forgot password

    //Todo: Reset password

	//Todo: Two factor authentification
}


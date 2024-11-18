using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.DTOs;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Identity;
using HopsHub.Api.Models;
using HopsHub.Api.Services;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
	private readonly IAccountService _accountService;

	private readonly UserManager<User> _userManager;

    private readonly IEmailService _emailService;

    public LoginController(IAccountService accountService, UserManager<User> user, IEmailService emailService)
	{
		_accountService = accountService;
		_userManager = user;
		_emailService = emailService;
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

        var confirmationLink = Url.Action(
            "ConfirmEmail",
            "Account",
            new { userId = result.UserId, token = result.Token },
            Request.Scheme
        );

        await _emailService.SendEmailAsync(
            loginDTO.Email,
            "Confirm your email",
            $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>"
        );

        return Ok(result.Message);
	}

	[HttpGet("/SendConfirmationEmail")]
	public IActionResult SendConfirmationEmail(string userId, string token)
    {
        var confirmationLink = Url.Action(
            "ConfirmEmail",
            "Account",
            new { userId, token },
            protocol: Request.Scheme
        );

		return Ok(confirmationLink);
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
	[HttpGet("/ConfirmEmail")]
	public async Task<IActionResult> ConfirmEmail(string userId, string token)
	{
		var user = await _userManager.FindByIdAsync(userId);
		if (user == null) return BadRequest("Invalid user ID.");

		var result = await _userManager.ConfirmEmailAsync(user, token);
		if (result.Succeeded) return Ok("Email confirmed successfully!");

		return BadRequest("Error confirming email.");
	}


	//Todo: Forgot password

	//Todo: Reset password

	//Todo: Two factor authentification
}


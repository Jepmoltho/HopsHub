﻿using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Shared.DTOs;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Identity;
using HopsHub.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HopsHub.Api.Shared;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	private readonly UserManager<User> _userManager;

    private readonly SignInManager<User> _signInManager;

    private readonly IEmailService _emailService;

    private readonly IConfiguration _configuration;

    public AccountController(IAccountService accountService, UserManager<User> user, IEmailService emailService, SignInManager<User> signInManager, IConfiguration configuration)
	{
		_accountService = accountService;
		_userManager = user;
		_emailService = emailService;
        _signInManager = signInManager;
        _configuration = configuration;
	}

    [EnableRateLimiting("NormalMaxRequestPolicy")]
    [HttpGet("/Users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _accountService.GetUsers();

        if (!result.Any())
        {
            return NotFound("No users found in the database");
        }

        return Ok(result);
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

        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (user == null)
        {
            return Unauthorized("User not found");
        }

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var jwtLoginKey = environment == "Production"
            ? System.IO.File.ReadAllText("/run/secrets/jwt_login_token_key").Trim()
            : Environment.GetEnvironmentVariable("JWT_LOGIN_TOKEN_KEY") ?? throw new InvalidOperationException("JWT_LOGIN_TOKEN_KEY not set");

        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER not set");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE not set");

        if (string.IsNullOrEmpty(jwtLoginKey))
        {
            throw new InvalidOperationException("Login token key is not set in environment variables.");
        }

        var keyBytes = Encoding.UTF8.GetBytes(jwtLoginKey);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new UserResult { Token = tokenString, UserId = user.Id, Message = "Login succesfull", Succeeded = true });
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

	[HttpGet("/ConfirmEmail")]
	public async Task<IActionResult> ConfirmEmail(string userId, string token)
	{
		var user = await _userManager.FindByIdAsync(userId);
		if (user == null) return BadRequest("Invalid user ID.");

		var result = await _userManager.ConfirmEmailAsync(user, token);
		if (result.Succeeded) return Ok("Email confirmed successfully!");

		return BadRequest("Error confirming email.");
	}


    //Todo: Request Reset password
    [EnableRateLimiting("HardMaxRequestPolicy")]
    [HttpPost("RequestPasswordReset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequestDTO resetRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _accountService.GeneratePasswordResetTokenAsync(resetRequest.Email);

        if (!result.Succeeded)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    //Todo: Reset Password
    [EnableRateLimiting("HardMaxRequestPolicy")]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _accountService.ResetPasswordAsync(resetPassword);

        if (!result.Succeeded)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    //Todo: Two factor authentification
}


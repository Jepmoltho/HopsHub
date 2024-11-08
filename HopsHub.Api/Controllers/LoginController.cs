﻿using Microsoft.AspNetCore.Mvc;
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

	[HttpPost]
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
}

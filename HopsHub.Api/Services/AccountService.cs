using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Api.Shared;
using HopsHub.Api.Constants;
using HopsHub.Api.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HopsHub.Api.Services;

public class AccountService : IAccountService
{
	private readonly SignInManager<User> _signInManager;

	public AccountService(SignInManager<User> signInManager)
	{
		_signInManager = signInManager;
	}

	public async Task<Result> LoginAsync(LoginDTO loginDTO)
	{
		var result = await _signInManager.PasswordSignInAsync(
			loginDTO.Email,
			loginDTO.Password,
			isPersistent: false,
			lockoutOnFailure: false);

		if (result.Succeeded)
		{
			return new Result { Succeeded = true, Message = LoginConstants.LoginSuccess }; 
		}

		return new Result { Succeeded = false, Message = LoginConstants.LoginFail };
	}
}


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

	private readonly UserManager<User> _userManager;

	public AccountService(SignInManager<User> signInManager, UserManager<User> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

	public async Task<Result> LoginAsync(LoginDTO loginDTO)
	{
		var user = await _userManager.FindByEmailAsync(loginDTO.Email);

		if (user == null || string.IsNullOrEmpty(user.UserName))
		{
            return new Result { Succeeded = false, Message = LoginConstants.IncorrectEmailPasswordCombination };
        }

        var result = await _signInManager.PasswordSignInAsync(
			user.UserName,
			loginDTO.Password,
			isPersistent: false,
			lockoutOnFailure: false);

		if (result.Succeeded)
		{
			return new Result { Succeeded = true, Message = LoginConstants.LoginSuccess }; 
		}

		return new Result { Succeeded = false, Message = LoginConstants.LoginFail };
	}

	public async Task<Result> LogoutAsync()
	{
		await _signInManager.SignOutAsync();

		return new Result { Succeeded = true, Message = LoginConstants.LogoutSuccess };
	}
}


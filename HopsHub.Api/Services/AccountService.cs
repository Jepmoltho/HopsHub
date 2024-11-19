using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Api.Shared;
using HopsHub.Api.Constants;
using HopsHub.Api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HopsHub.Api.Services;

public class AccountService : ControllerBase, IAccountService
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

    public async Task<UserResult> CreateUser(LoginDTO loginDTO)
    {
        var user = new User
        {
            UserName = loginDTO.Email,
            Email = loginDTO.Email
        };

        var createdUser = await _userManager.CreateAsync(user, loginDTO.Password);

        if (!createdUser.Succeeded)
        {
            return new UserResult { Succeeded = false, Message = $"{LoginConstants.UserCreatedFail}: {createdUser.Errors.Select(error => error.Description)}" };
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return new UserResult { Succeeded = true, Message = LoginConstants.UserCreatedSuccess, Token = token, UserId = user.Id };
    }

    public async Task<Result> DeleteUser(DeleteUserDTO deleteUserDTO)
    {
		var user = await _userManager.FindByIdAsync(deleteUserDTO.Id.ToString());

        if (user == null)
        {
            return new Result { Succeeded = false, Message = LoginConstants.UserDeleteFail };
        }

		var userEmail = await _userManager.GetEmailAsync(user); 

		if (userEmail != deleteUserDTO.Email)
		{
            return new Result { Succeeded = false, Message = LoginConstants.UserDeleteFail };
        }

        var passwordCheck = await _userManager.CheckPasswordAsync(user, deleteUserDTO.Password);

		if (!passwordCheck)
		{
            return new Result { Succeeded = false, Message = LoginConstants.UserDeleteFail };
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return new Result
            {
                Succeeded = false,
                Message = $"{LoginConstants.UserDeleteFail}: {result.Errors.Select(e => e.Description)}"
            };
        }

        return new Result { Succeeded = true, Message = LoginConstants.UserDeleteSuccess };
    }
}
using HopsHub.Api.DTOs;
using HopsHub.Api.Shared;
using HopsHub.Api.Models;

namespace HopsHub.Api.Services.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<User>> GetUsers();
    Task<Result> LoginAsync(LoginDTO loginDTO);
    Task<Result> LogoutAsync();
    Task<UserResult> CreateUser(LoginDTO loginDTO);
    Task<Result> DeleteUser(DeleteUserDTO deleteUseDTO);
    Task<Result> GeneratePasswordResetTokenAsync(string email);
    Task<Result> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
}


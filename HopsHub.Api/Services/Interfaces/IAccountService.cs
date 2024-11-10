using HopsHub.Api.DTOs;
using HopsHub.Api.Shared;

namespace HopsHub.Api.Services.Interfaces;

public interface IAccountService
{
    Task<Result> LoginAsync(LoginDTO loginDTO);
    Task<Result> LogoutAsync();
    Task<Result> CreateUser(LoginDTO loginDTO);
}


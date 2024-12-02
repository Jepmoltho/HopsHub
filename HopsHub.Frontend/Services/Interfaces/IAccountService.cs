using HopsHub.Shared;
using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services.Interfaces;

public interface IAccountService
{
    Task<Result> CreateUserAsync(CreateUserDTO createUserDTO);

    Task<Result> LoginUserAsync(LoginDTO loginDTO);
}


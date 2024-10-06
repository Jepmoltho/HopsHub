using HopsHub.Api.Models;

namespace HopsHub.Api.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<User>> GetUsers();
}


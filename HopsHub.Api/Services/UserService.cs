using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace HopsHub.Api.Services;


public class UserService : IUserService
{
	private readonly IRepository<User> _userRepository;

	public UserService(IRepository<User> userRepository)
	{
		_userRepository = userRepository;
	}

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _userRepository.GetQuerable()
            .Include(u => u.Ratings)
            .ToListAsync();
    }
}


using HopsHub.Api.Models;
namespace HopsHub.Api.Interfaces;

public interface IBeerService
{
	Task<List<Beer>> GetBeers();
    Task<List<Rating>> GetRatings();
    Task<List<Models.Type>> GetTypes();
    Task<List<User>> GetUsers();
    Task<List<Beer>> GetBeersByType(int typeId);
    Task<List<Rating>> GetRatingsByUser(Guid userId);
    Task<List<Rating>> GetRatingsByUserAndType(Guid userId, int typeId);
}


using HopsHub.Api.Models;
namespace HopsHub.Api.Services.Interfaces;

public interface IRatingsService
{
    Task<List<Rating>> GetRatings();
    Task<List<Rating>> GetRatingsByUser(Guid userId);
    Task<List<Rating>> GetRatingsByUserAndType(Guid userId, int typeId);
}


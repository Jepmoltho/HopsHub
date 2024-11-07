using HopsHub.Api.Models;
using HopsHub.Api.DTOs;
namespace HopsHub.Api.Services.Interfaces;

public interface IRatingsService
{
    Task<List<Rating>> GetRatings();
    Task<List<Rating>> GetRatingsByUser(Guid userId);
    Task<List<Rating>> GetRatingsByUserAndType(Guid userId, int typeId);
    Task<Rating> PostRating(RatingDTO ratingDTO);
    Task<Rating> UpdateRating(UpdateRatingDTO ratingDTO);
    Task<Rating> DeleteRating(DeleteRatingDTO ratingDTO);
}


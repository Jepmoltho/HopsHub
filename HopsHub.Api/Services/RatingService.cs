using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Api.DTOs;
using HopsHub.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

public class RatingService : IRatingsService
{
    private readonly IRepository<Rating> _ratingRepository;

    public RatingService(IRepository<Rating> ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<List<Rating>> GetRatings()
    {
        return await _ratingRepository.GetAllAsync();
    }

    public async Task<List<Rating>> GetRatingsByUser(Guid userId)
    {
        var ratings = await _ratingRepository.GetQuerable()
                                .Where(r => r.UserId == userId)
                                .Include(r => r.Beer)
                                .ToListAsync();

        return ratings;
    }

    public async Task<List<Rating>> GetRatingsByUserAndType(Guid userId, int typeId)
    {
        var ratings = await _ratingRepository.GetQuerable()
                        .Where(r => r.UserId == userId)
                        .Include(r => r.Beer)
                        .Where(r => r.Beer.TypeId == typeId)
                        .ToListAsync();

        return ratings;
    }

    public async Task<Rating> PostRating(RatingDTO ratingDTO)
    {
        var rating = new Rating
        {
            BeerId = ratingDTO.BeerId,
            Score = ratingDTO.Score,
            UserId = ratingDTO.UserId,
            Comment = ratingDTO.Comment
        };

        var exist = await _ratingRepository.ExistAsync(r => r.UserId == rating.UserId && r.BeerId == rating.BeerId);

        if (exist)
        {
            throw new EntityExistsException($"Beer {rating.BeerId} is already rated by user {rating.UserId}");
        }

        await _ratingRepository.AddAsync(rating);

        await _ratingRepository.SaveAsync();

        return rating;
    }
}

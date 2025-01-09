using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Shared.DTOs;
using HopsHub.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Helpers;
using HopsHub.Api.Repositories.Interfaces;

public class RatingService : IRatingsService
{
    private readonly IRepository<Rating> _ratingRepository;
    private readonly IRepository<Beer> _beerRepository;

    public RatingService(IRepository<Rating> ratingRepository, IRepository<Beer> beerRepository)
    {
        _ratingRepository = ratingRepository;
        _beerRepository = beerRepository;
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
        var ratings = new List<Rating>();

        //typeid 0: beers of all types
        if (typeId == 0)
        {
            ratings = await _ratingRepository.GetQuerable()
                                .Where(r => r.UserId == userId)
                                .Include(r => r.Beer)
                                .ToListAsync();
        }
        else
        {
            ratings = await _ratingRepository.GetQuerable()
                .Where(r => r.UserId == userId)
                .Include(r => r.Beer)
                .Where(r => r.Beer.TypeId == typeId)
                .ToListAsync();
        }

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

        await UpdateBeerAverageScore(rating.BeerId);
        
        return rating;
    }

    public async Task<List<Rating>> GetRatingsByBeerId(int beerId)
    {
        var ratings = await _ratingRepository.GetQuerable()
                                .Where(r => r.BeerId == beerId)
                                .ToListAsync();

        if (!ratings.Any())
        {
            return new List<Rating>();
        }

        return ratings;
    }

    private async Task UpdateBeerAverageScore(int beerId)
    {
        var ratingsForBeer = await GetRatingsByBeerId(beerId);

        if (ratingsForBeer.Any())
        {
            var average = ratingsForBeer.Average(r => r.Score);
            var beer = await _beerRepository.GetByIdAsync(beerId);

            if (beer != null)
            {
                beer.AverageScore = average;
                _beerRepository.Update(beer);
                await _beerRepository.SaveAsync();
            }
        }
    }

    public async Task<Rating> UpdateRating(UpdateRatingDTO ratingDTO)
    {
        var rating = await _ratingRepository.GetByLongIdAsync(ratingDTO.Id);

        if (rating == null)
        {
            throw new EntityNotFoundException($"Rating {ratingDTO.Id} not found in database");
        }

        UpdateHelper.UpdateEntity(ratingDTO, rating);

        _ratingRepository.Update(rating);
        await _ratingRepository.SaveAsync();

        return rating;
    }

    public async Task<Rating> DeleteRating(DeleteRatingDTO ratingDTO)
    {
        var rating = await _ratingRepository.GetByLongIdAsync(ratingDTO.Id);

        if (rating == null)
        {
            throw new EntityNotFoundException($"Rating {ratingDTO.Id} not found in database");
        }

        UpdateHelper.UpdateEntity(ratingDTO, rating);

        _ratingRepository.Update(rating);
        await _ratingRepository.SaveAsync();

        return rating;
    }
}

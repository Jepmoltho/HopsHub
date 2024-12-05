using System;
using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services.Interfaces;

public interface IRatingService
{
    Task<List<RatingDTO>> GetAllPrivateRatingsAsync();

    Task<List<RatingDTO>> GetAllPrivateRatingsByTypeAsync(int typeId);
}


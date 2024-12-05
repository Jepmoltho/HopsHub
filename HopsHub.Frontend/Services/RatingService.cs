using System;
using System.Net.Http.Json;
using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services;

public class RatingService : IRatingService
{
	private readonly HttpClient _httpClient;

	public RatingService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

    public async Task<List<RatingDTO>> GetAllPublicRatingsAsync()
    {
        var response = await _httpClient.GetAsync("Ratings");

        if (response.IsSuccessStatusCode)
        {
            var ratings = await response.Content.ReadFromJsonAsync<List<RatingDTO>>();

            return ratings ?? new List<RatingDTO>();
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<RatingDTO>();
        }

        throw new Exception($"Failed to fetch ratings. Status code: {response.StatusCode}");
    }

    public async Task<List<RatingDTO>> GetAllPrivateRatingsAsync(Guid userId)
    {
        var response = await _httpClient.GetAsync($"Ratings/{userId}");

        if (response.IsSuccessStatusCode)
        {
            var ratings = await response.Content.ReadFromJsonAsync<List<RatingDTO>>();

            return ratings ?? new List<RatingDTO>();
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<RatingDTO>();
        }

        throw new Exception($"Failed to fetch ratings. Status code: {response.StatusCode}");
    }
}


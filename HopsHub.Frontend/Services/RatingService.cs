using System;
using System.Net.Http.Json;
using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared.DTOs;
using Blazored.LocalStorage;

namespace HopsHub.Frontend.Services;

public class RatingService : IRatingService
{
	private readonly HttpClient _httpClient;

    private readonly ILocalStorageService _localStorage;

	public RatingService(HttpClient httpClient, ILocalStorageService localStorage)
	{
		_httpClient = httpClient;
        _localStorage = localStorage;
	}

    //Todo: Delete - There is never a situation where this is relevant
    //public async Task<List<RatingDTO>> GetAllPublicRatingsAsync()
    //{
    //    var response = await _httpClient.GetAsync("Ratings");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var ratings = await response.Content.ReadFromJsonAsync<List<RatingDTO>>();

    //        return ratings ?? new List<RatingDTO>();
    //    }

    //    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
    //    {
    //        return new List<RatingDTO>();
    //    }

    //    throw new Exception($"Failed to fetch ratings. Status code: {response.StatusCode}");
    //}



    public async Task<List<RatingDTO>> GetAllPrivateRatingsAsync()
    {
        var userId = await _localStorage.GetItemAsync<Guid>("userId");

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

    public async Task<List<RatingDTO>> GetAllPrivateRatingsByTypeAsync(int typeId)
    {
        var userId = await _localStorage.GetItemAsync<Guid>("userId");

        var response = await _httpClient.GetAsync($"Ratings/{userId}/{typeId}");

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


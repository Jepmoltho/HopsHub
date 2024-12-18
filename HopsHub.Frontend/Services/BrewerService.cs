using System.Net.Http.Json;
using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HopsHub.Frontend.Services;

public class BrewerService : IBrewerService
{
    private readonly HttpClient _httpClient;

    public BrewerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BrewerDTO>> GetBrewersAsync()
    {
        var response = await _httpClient.GetAsync("Brewers");

        if (response.IsSuccessStatusCode)
        {
            var brewers = await response.Content.ReadFromJsonAsync<List<BrewerDTO>>();

            return brewers ?? new List<BrewerDTO>();
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<BrewerDTO>();
        }

        throw new Exception($"Failed to fetch brewers. Status code: {response.StatusCode}");
    }

    public async Task<BrewerDTO> PostBrewerAsync(AddBrewerDTO brewer)
    {
        var response = await _httpClient.PostAsJsonAsync("/Brewer", brewer);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("You are not authorized to add a brewer.");
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"Conflict occurred: {errorMessage}");
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to add brewer. Status code: {response.StatusCode}. Details: {errorDetails}");
        }

        var brewerResult = await response.Content.ReadFromJsonAsync<BrewerDTO>();

        if (brewerResult == null)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create brewer. Status code: {response.StatusCode}. Details: {errorDetails}");
        }

        return brewerResult;
    }
}


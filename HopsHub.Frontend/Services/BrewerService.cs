using System.Net.Http.Json;
using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared.DTOs;

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
}


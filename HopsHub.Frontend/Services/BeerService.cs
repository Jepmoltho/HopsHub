using HopsHub.Shared.DTOs;
using HopsHub.Frontend.Services.Interfaces;
using System.Net.Http.Json;

namespace HopsHub.Frontend.Services;

public class BeerService : IBeerService
{
	private readonly HttpClient _httpClient;

	public BeerService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<BeerDTO>> GetBeersAsync()
	{
		var response = await _httpClient.GetAsync("Beers");

		if (response.IsSuccessStatusCode)
		{
			var beers = await response.Content.ReadFromJsonAsync<List<BeerDTO>>();

			return beers ?? new List<BeerDTO>();
		}

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<BeerDTO>();
        }

        throw new Exception($"Failed to fetch beers. Status code: {response.StatusCode}");
    }

    public async Task<List<SelectBeerDTO>> GetSelectBeerAsync()
    {
        var response = await _httpClient.GetAsync("BeersBrewersTypes");

        if (response.IsSuccessStatusCode)
        {
            var beers = await response.Content.ReadFromJsonAsync<List<SelectBeerDTO>>();

            return beers ?? new List<SelectBeerDTO>();
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<SelectBeerDTO>();
        }

        throw new Exception($"Failed to fetch beers. Status code: {response.StatusCode}");
    }

    public async Task<List<BeerDTO>> GetBeerByTypeAsync(int typeId)
	{
        var response = await _httpClient.GetAsync($"Beers/{typeId}");

        if (response.IsSuccessStatusCode)
        {
            var beers = await response.Content.ReadFromJsonAsync<List<BeerDTO>>();

            return beers ?? new List<BeerDTO>();
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<BeerDTO>();
        }

        throw new Exception($"Failed to fetch beers by type {typeId}. Status code: {response.StatusCode}");
    }
}


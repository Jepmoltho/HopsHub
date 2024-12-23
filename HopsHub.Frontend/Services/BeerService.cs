using HopsHub.Shared.DTOs;
using HopsHub.Frontend.Services.Interfaces;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<SelectBeerDTO> PostBeerAsync(AddBeerDTO beer)
    {
        var response = await _httpClient.PostAsJsonAsync("/Beer", beer);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("You are not authorized to add a beer.");
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"Conflict occurred: {errorMessage}");
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to add beer. Status code: {response.StatusCode}. Details: {errorDetails}");
        }

        var beerResponse = await response.Content.ReadFromJsonAsync<SelectBeerDTO>();

        if (beerResponse == null)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create brewer. Status code: {response.StatusCode}. Details: {errorDetails}");
        }

        return beerResponse;
    }
}


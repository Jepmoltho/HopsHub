using HopsHub.Api.Models;
using HopsHub.Shared.DTOs;

namespace HopsHub.Api.Services.Interfaces;

public interface IBeerService
{
	Task<List<Beer>> GetBeers();
    Task<List<Beer>> GetBeersBrewersTypes();
    Task<List<Beer>> GetBeersByType(int typeId);
    Task<Beer> PostBeer(BeerDTO beer);
    Task<Beer> UpdateBeer(UpdateBeerDTO beerDTO);
    Task<Beer> DeleteBeer(DeleteBeerDTO beerDTO);
}


using HopsHub.Api.Models;
using HopsHub.Api.Shared;

namespace HopsHub.Api.Services.Interfaces;

public interface IBeerService
{
	Task<List<Beer>> GetBeers();
    Task<List<Beer>> GetBeersByType(int typeId);
    Task<Result<Beer>> PostBeer(Beer beer);
}


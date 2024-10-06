using HopsHub.Api.Models;
namespace HopsHub.Api.Interfaces;

public interface IBeerService
{
	Task<List<Beer>> GetBeers();
    Task<List<Beer>> GetBeersByType(int typeId);
}


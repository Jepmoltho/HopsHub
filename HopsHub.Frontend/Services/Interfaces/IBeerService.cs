using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services.Interfaces;

public interface IBeerService
{
    Task<List<BeerDTO>> GetBeersAsync();
    Task<List<BeerBrewerTypeDTO>> GetBeersBrewersTypesAsync();
    Task<List<BeerDTO>> GetBeerByTypeAsync(int typeId);
    //Task<BeerDTO> PostBeer(BeerDTO addBeerDTO);
}


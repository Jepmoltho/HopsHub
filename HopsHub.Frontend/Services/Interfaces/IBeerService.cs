using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services.Interfaces;

public interface IBeerService
{
    Task<List<BeerDTO>> GetBeersAsync();
}


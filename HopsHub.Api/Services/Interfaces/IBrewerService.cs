using HopsHub.Shared.DTOs;
using HopsHub.Api.Models;

namespace HopsHub.Api.Services.Interfaces;

public interface IBrewerService
{
    Task<List<Brewer>> GetBrewers();
    Task<Brewer> PostBrewer(BrewerDTO brewerDTO);
    Task<Brewer> PutBrewer(UpdateBrewerDTO updateBrewerDTO);
    Task<Brewer> DeleteBrewer(DeleteBrewerDTO brewerDTO);
}

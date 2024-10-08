using HopsHub.Api.DTOs;
using HopsHub.Api.Models;

namespace HopsHub.Api.Services.Interfaces;

public interface IBrewerService
{
    Task<List<Brewer>> GetBrewers();
    Task<Brewer> PostBrewer(BrewerDTO brewerDTO);
}

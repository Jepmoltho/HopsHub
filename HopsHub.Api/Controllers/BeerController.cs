using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Data;
using HopsHub.Api.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class BeerController : ControllerBase
{
    //To do: Implement service layer between controller and context
    private readonly BeerContext _beerContext;

    public BeerController(BeerContext beerContext)
    {
        _beerContext = beerContext;
    }

    [HttpGet("/Beers")]
    public async Task<List<Beer>> GetBeers()
    {
        return await _beerContext.Beers
            .Include(b => b.Type)
            .Include(b => b.Ratings)
            .ToListAsync();
    }

    [HttpGet("/Ratings")]
    public async Task<List<Rating>> GetRatings()
    {
        return await _beerContext.Ratings
            .ToListAsync();
    }

    [HttpGet("/Types")]
    public async Task<List<HopsHub.Api.Models.Type>> GetTypes()
    {
        return await _beerContext.Types
            .ToListAsync();
    }

    [HttpGet("/Beers/Type")]
    public async Task<List<Beer>> GetBeersByType(int typeId)
    {
        return await _beerContext.Beers
            .Where(b => b.TypeId == typeId)
            .Include(b => b.Type)
            .ToListAsync();
    }

    [HttpGet("/Beers/Type/UserId")]
    public async Task<List<Beer>> GetBeersByTypeAndUserId(int typeId, Guid userId)
    {
        return await _beerContext.Beers
            .Where(b => b.TypeId == typeId)
            .ToListAsync();
    }
}
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
            .ToListAsync();
    }

    [HttpGet("/Types")]
    public async Task<List<HopsHub.Api.Models.Type>> GetTypes()
    {
        return await _beerContext.Types.ToListAsync();
    }
}
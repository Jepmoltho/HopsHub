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

    [HttpGet]
    public async Task<List<Beer>> GetBeers()
    {
        return await _beerContext.Beers.ToListAsync();
    }
}
using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Services;


[ApiController]
[Route("[controller]")]
public class BeerController : ControllerBase
{
    private readonly BeerService _beerService;

    public BeerController(BeerService beerService)
    {
        _beerService = beerService;
    }

    // Get all methods
    [HttpGet("/Beers")]
    public async Task<IActionResult> GetBeers()
    {
        var result = await _beerService.GetBeers();

        if (!result.Any())
        {
            return NotFound("No beers found in the database");
        }

        return Ok(result);
    }

    // Get all ratings
    [HttpGet("/Ratings")]
    public async Task<IActionResult> GetRatings()
    {
        var result = await _beerService.GetRatings();

        if (!result.Any())
        {
            return NotFound("No ratings found in the database");
        }

        return Ok(result);
    }

    // Get all types
    [HttpGet("/Types")]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _beerService.GetTypes();

        if (!result.Any())
        {
            return NotFound("No types found in the database");
        }

        return Ok(result);
    }

    // Get all users
    [HttpGet("/Users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _beerService.GetUsers();

        if (!result.Any())
        {
            return NotFound("No users found in the database");
        }

        return Ok(result);
    }

    [HttpGet("/Beers/{typeId}")]
    public async Task<IActionResult> GetBeersByType(int typeId)
    {
        var result = await _beerService.GetBeersByType(typeId);

        if (!result.Any())
        {
            return NotFound($"No beers found for type with id {typeId}");
        }

        return Ok(result);
    }
}
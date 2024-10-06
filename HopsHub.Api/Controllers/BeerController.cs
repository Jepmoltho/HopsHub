using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Interfaces;
using HopsHub.Api.Services.Interfaces;


[ApiController]
[Route("[controller]")]
public class BeerController : ControllerBase
{
    private readonly IBeerService _beerService;
    private readonly ITypeService _typeService;
    private readonly IUserService _userService;
    private readonly IRatingsService _ratingService;

    public BeerController(IBeerService beerService, ITypeService typeService, IUserService userService, IRatingsService ratingService)
    {
        _beerService = beerService;
        _typeService = typeService;
        _userService = userService;
        _ratingService = ratingService;
    }

    // Get all beers
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
        var result = await _ratingService.GetRatings();

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
        var result = await _typeService.GetTypes();

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
        var result = await _userService.GetUsers();

        if (!result.Any())
        {
            return NotFound("No users found in the database");
        }

        return Ok(result);
    }

    //Get all beers by type
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

    //Get all ratings by user
    [HttpGet("/Ratings/{userId}")]
    public async Task<IActionResult> GetRatingsByUser(Guid userId)
    {
        var result = await _ratingService.GetRatingsByUser(userId);

        if (!result.Any())
        {
            return NotFound($"No ratings found for {userId}");
        }

        return Ok(result);
    }

    //Get all beers by user and type
    [HttpGet("/Ratings/{userId}/{typeId}")]
    public async Task<IActionResult> GetRatingsByUserAndType(Guid userId, int typeId) {

        var result = await _ratingService.GetRatingsByUserAndType(userId, typeId);

        if (!result.Any())
        {
            return NotFound($"No beers found for type {typeId} and user {userId}");
        }

        return Ok(result);
    }

    //Post a beer
    //[HttpPost("/Beers")]
    //public async Task<IActionResult> PostBeer(Guid userId, int typeId, string name, string brewer)
    //{
    //    var result = await _beerService.

    //}
}


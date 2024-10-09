using Microsoft.AspNetCore.Mvc;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Exceptions;
using HopsHub.Api.DTOs;
using HopsHub.Api.Helpers;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class BeerController : ControllerBase
{
    private readonly IBeerService _beerService;
    private readonly ITypeService _typeService;
    private readonly IUserService _userService;
    private readonly IRatingsService _ratingService;
    private readonly IBrewerService _brewerService;

    public BeerController(IBeerService beerService, ITypeService typeService, IUserService userService, IRatingsService ratingService, IBrewerService brewerService)
    {
        _beerService = beerService;
        _typeService = typeService;
        _userService = userService;
        _ratingService = ratingService;
        _brewerService = brewerService;
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

    // Get all brewers
    [HttpGet("/Brewers")]
    public async Task<IActionResult> GetBrewers()
    {
        var result = await _brewerService.GetBrewers();

        if (!result.Any())
        {
            return NotFound("No brewers found in the database");
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
    [HttpPost("/Beer")]
    public async Task<IActionResult> PostBeer([FromBody] BeerDTO beerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _beerService.PostBeer(beerDTO);

            return Ok(result);

        }
        catch (EntityExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (UserNotExistsException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, "An unhandled exception occured");
        }
    }

    [HttpPost("/Rating")]
    public async Task<IActionResult> PostRating(RatingDTO ratingDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _ratingService.PostRating(ratingDTO);

            return Ok(result);
        }
        catch (EntityExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, "An unhandled exception occured");
        }
    }

    [HttpPost("/Brewer")]
    public async Task<IActionResult> PostBrewer(BrewerDTO brewerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _brewerService.PostBrewer(brewerDTO);

            return Ok(result);
        }
        catch (EntityExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, "An unhandled exception occured");
        }
    }

    [HttpPut("/Beer")]
    public async Task<IActionResult> UpdateBeer(UpdateBeerDTO beerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _beerService.UpdateBeer(beerDTO);
            return Ok(result);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (InvalidArgumentException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, "An unhandled exception occurred");
        }
    }

    [HttpPut("/Rating")]
    public async Task<IActionResult> UpdateRating(UpdateRatingDTO ratingDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _ratingService.UpdateRating(ratingDTO);
            return Ok(result);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (InvalidArgumentException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ExceptionHelper.PrintMessage(ex.Message, ex.InnerException?.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, "An unhandled exception occurred");
        }
    }

}


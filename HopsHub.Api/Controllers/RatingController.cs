using HopsHub.Api.DTOs;
using HopsHub.Api.Exceptions;
using HopsHub.Api.Helpers;
using HopsHub.Api.Services;
using HopsHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
    private readonly IRatingsService _ratingService;

    public RatingController(IRatingsService ratingsService)
    {
        _ratingService = ratingsService;
    }

    [EnableRateLimiting("SoftMaxRequestPolicy")]
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

    [EnableRateLimiting("NormalMaxRequestPolicy")]
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

    [EnableRateLimiting("NormalMaxRequestPolicy")]
    [HttpGet("/Ratings/{userId}/{typeId}")]
    public async Task<IActionResult> GetRatingsByUserAndType(Guid userId, int typeId)
    {

        var result = await _ratingService.GetRatingsByUserAndType(userId, typeId);

        if (!result.Any())
        {
            return NotFound($"No beers found for type {typeId} and user {userId}");
        }

        return Ok(result);
    }

    [EnableRateLimiting("SoftMaxRequestPolicy")]
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

    [EnableRateLimiting("SoftMaxRequestPolicy")]
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

    [EnableRateLimiting("NormalMaxRequestPolicy")]
    [HttpDelete("/Rating")]
    public async Task<IActionResult> DeleteRating(DeleteRatingDTO deleteRatingDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _ratingService.DeleteRating(deleteRatingDTO);
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


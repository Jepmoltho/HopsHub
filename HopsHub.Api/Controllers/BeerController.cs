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

    public BeerController(IBeerService beerService)
    {
        _beerService = beerService;
    }

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

    [HttpDelete("/Beer")]
    public async Task<IActionResult> DeleteBeer(DeleteBeerDTO deleteBeerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _beerService.DeleteBeer(deleteBeerDTO); 
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


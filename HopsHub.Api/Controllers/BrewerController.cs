using HopsHub.Shared.DTOs;
using HopsHub.Api.Exceptions;
using HopsHub.Api.Helpers;
using HopsHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewerController : ControllerBase
{
    private readonly IBrewerService _brewerService;

    public BrewerController(IBrewerService brewerService)
	{
        _brewerService = brewerService;
	}

    [EnableRateLimiting("NormalMaxRequestPolicy")]
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

    [EnableRateLimiting("HardMaxRequestPolicy")]
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

    [EnableRateLimiting("HardMaxRequestPolicy")]
    [HttpPut("/Brewer")]
    public async Task<IActionResult> UpdateBrewer(UpdateBrewerDTO updateBrewerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _brewerService.PutBrewer(updateBrewerDTO);
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

    [EnableRateLimiting("HardMaxRequestPolicy")]
    [HttpDelete("/Brewer")]
    public async Task<IActionResult> DeleteBrewer(DeleteBrewerDTO deleteBrewerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _brewerService.DeleteBrewer(deleteBrewerDTO);
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


using HopsHub.Api.DTOs;
using HopsHub.Api.Exceptions;
using HopsHub.Api.Helpers;
using HopsHub.Api.Services;
using HopsHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TypeController : ControllerBase
{
    private readonly ITypeService _typeService;

    public TypeController(ITypeService typeService)
	{
        _typeService = typeService;
	}

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

    [HttpPost("/Type")]
    public async Task<IActionResult> PostType(TypeDTO typeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _typeService.PostType(typeDTO);

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

    [HttpPut("/Type")]
    public async Task<IActionResult> UpdateType(UpdateTypeDTO updateTypeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _typeService.PutType(updateTypeDTO);
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

    [HttpDelete("/Type")]
    public async Task<IActionResult> DeleteType(DeleteTypeDTO deleteTypeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _typeService.DeleteType(deleteTypeDTO);
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


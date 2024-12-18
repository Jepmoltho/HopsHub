using System;
using HopsHub.Shared.DTOs;
using Microsoft.AspNetCore;
namespace HopsHub.Frontend.Services.Interfaces;

public interface IBrewerService
{
    Task<List<BrewerDTO>> GetBrewersAsync();

    Task<BrewerDTO> PostBrewerAsync(AddBrewerDTO brewer);
}


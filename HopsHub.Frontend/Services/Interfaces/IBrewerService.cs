using System;
using HopsHub.Shared.DTOs;
namespace HopsHub.Frontend.Services.Interfaces;

public interface IBrewerService
{
    Task<List<BrewerDTO>> GetBrewersAsync();
}


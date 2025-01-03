﻿using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services.Interfaces;

public interface IBeerService
{
    Task<List<BeerDTO>> GetBeersAsync();
    Task<List<SelectBeerDTO>> GetSelectBeerAsync();
    Task<List<BeerDTO>> GetBeerByTypeAsync(int typeId);
    Task<SelectBeerDTO> PostBeerAsync(AddBeerDTO beer);
}


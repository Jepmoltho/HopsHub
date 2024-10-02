using System;
using HopsHub.Api.Data;
using HopsHub.Api.Interfaces;
using HopsHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Services;

public class BeerService : IBeerService
{
	private readonly BeerContext _beerContext;

    public BeerService(BeerContext beerContext)
	{
		_beerContext = beerContext;
	}

	public async Task<List<Beer>> GetBeers()
	{
		return await _beerContext.Beers
            .Include(b => b.Type)
            .Include(b => b.Ratings)
            .ToListAsync();
    }

    public async Task<List<Rating>> GetRatings()
    {
        return await _beerContext.Ratings
            .ToListAsync();
    }

    public async Task<List<Models.Type>> GetTypes()
    {
        return await _beerContext.Types
            .ToListAsync();
    }

    public async Task<List<User>> GetUsers()
    {
        return await _beerContext.Users
            .Include(u => u.Ratings)
            .ToListAsync();
    }

    public async Task<List<Beer>> GetBeersByType(int typeId)
    {
        var typeExist = await _beerContext.Types
                    .Where(t => t.Id == typeId)
                    .AnyAsync();

        if (!typeExist)
        {
            return new List<Beer>();
        }

        //To do: Fix nullable type
        var beers = await _beerContext.Beers
                        .Include(b => b.Type)
                        .Where(t => t.Type.Id == typeId)
                        .ToListAsync();

        return beers;
    }
}


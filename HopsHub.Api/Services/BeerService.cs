using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Shared;

namespace HopsHub.Api.Services;

public class BeerService : IBeerService
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerService(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<List<Beer>> GetBeers()
    {
        return await _beerRepository.GetQuerable()
            .Include(b => b.Type)
            .Include(b => b.Ratings)
            .ToListAsync();
    }


    public async Task<List<Beer>> GetBeersByType(int typeId)
    {
        //To do: Fix nullable type
        var beers = await _beerRepository.GetQuerable()
                        .Include(b => b.Type)
                        .Where(b => b.Type.Id == typeId)
                        .ToListAsync();

        return beers;
    }


    public async Task<Result<Beer>> PostBeer(Beer beer)
    {
        var nameLowerCase = beer.Name.ToLower();

        var exist = await _beerRepository.ExistAsync(b => b.Name == beer.Name);

        if (exist)
        {
            return new Result<Beer>(false, beer, 500, "A beer with that name already exist");
        }

        await _beerRepository.AddAsync(beer);

        await _beerRepository.SaveAsync();

        return new Result<Beer>(true, beer, 0, "Succesfully added beer to the database");        
    }

}


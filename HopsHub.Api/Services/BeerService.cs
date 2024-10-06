using HopsHub.Api.Interfaces;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using Microsoft.EntityFrameworkCore;

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

    //public async Task<Result<Beer>> PostBeer(string name, int typeId, string brewer, decimal alc)
    //{
    //    var nameLowerCase = name.ToLower();

    //    var exist = await _beerContext.Beers.AnyAsync(b => b.Name.ToLower() == nameLowerCase);

    //    if (exist)
    //    {
    //        var beer = await _beerContext.Beers.FirstAsync(b => b.Name.ToLower() == nameLowerCase);

    //        return new Result<Beer>(false, beer, 500, "A beer with that name already exist");

    //        //return await _beerContext.Beers.FirstAsync(b => b.Name.ToLower() == name.ToLower()); 
    //    }
    //}

}


using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Api.Exceptions;
using HopsHub.Api.DTOs;
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


    public async Task<Beer> PostBeer(BeerDTO beerDTO)
    {

        var beer = new Beer
        {
            Name = beerDTO.Name,
            TypeId = beerDTO.TypeId,
            Alc = beerDTO.Alc,
            Description = beerDTO.Description,
            BrewerId = beerDTO.BrewerId
        };

        var exist = await _beerRepository.ExistAsync(b => b.Name.ToLower() == beer.Name.ToLower());

        if (exist)
        {
            throw new EntityExistsException("Beer already exist");
        }

        await _beerRepository.AddAsync(beer);

        await _beerRepository.SaveAsync();

        return beer;
    }
}


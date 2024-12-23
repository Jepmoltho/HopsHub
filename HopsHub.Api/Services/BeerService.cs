using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Api.Exceptions;
using HopsHub.Shared.DTOs;
using HopsHub.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Repositories.Interfaces;

namespace HopsHub.Api.Services;

public class BeerService : IBeerService
{
    private readonly IRepository<Beer> _beerRepository;
    private readonly IRepository<User> _userRepository;

    public BeerService(IRepository<Beer> beerRepository, IRepository<User> userRepository)
    {
        _beerRepository = beerRepository;
        _userRepository = userRepository;

    }

    public async Task<List<Beer>> GetBeers()
    {
        return await _beerRepository.GetQuerable()
            .Include(b => b.Type)
            .Include(b => b.Ratings)
            .Include(b => b.Brewer)
            .ToListAsync();
    }

    public async Task<List<Beer>> GetBeersBrewersTypes()
    {
        return await _beerRepository.GetQuerable()
                    .Include(b => b.Type)
                    .Include(b => b.Brewer)
                    .ToListAsync();
    }

    public async Task<List<Beer>> GetBeersByType(int typeId)
    {
        //Todo: Fix nullable type
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
            BrewerId = beerDTO.BrewerId,
            CreatedByUser = beerDTO.UserId
        };

        var exist = await _beerRepository.ExistAsync(b => b.Name.ToLower() == beer.Name.ToLower());

        var userExist = await _userRepository.ExistAsync(u => u.Id == beer.CreatedByUser);

        if (exist)
        {
            throw new EntityExistsException("Beer already exist");
        }

        if (!userExist)
        {
            throw new UserNotExistsException("User does not exist");
        }

        await _beerRepository.AddAsync(beer);

        await _beerRepository.SaveAsync();

        var createdBeer = await _beerRepository
            .GetQuerable()
            .Include(b => b.Type)
            .Include(b => b.Brewer)
            .FirstOrDefaultAsync(b => b.Name.ToLower() == beer.Name.ToLower() && b.BrewerId == beer.BrewerId);


        if (createdBeer == null)
        {
            throw new Exception("Failed to fetch the newly created beer with related data.");
        }

        return createdBeer;
    }

    public async Task<Beer> UpdateBeer(UpdateBeerDTO beerDTO)
    {
        var beer = await _beerRepository.GetByIdAsync(beerDTO.Id);

        if (beer == null)
        {
            throw new EntityNotFoundException($"Beer {beerDTO.Id} not found in database");
        }

        UpdateHelper.UpdateEntity(beerDTO, beer);

        _beerRepository.Update(beer);
        await _beerRepository.SaveAsync();

        return beer;
    }

    public async Task<Beer> DeleteBeer(DeleteBeerDTO beerDTO)
    {
        var beer = await _beerRepository.GetByIdAsync(beerDTO.Id);

        if (beer == null)
        {
            throw new EntityNotFoundException($"Beer {beerDTO.Id} not found in database");
        }

        UpdateHelper.UpdateEntity(beerDTO, beer);

        _beerRepository.Update(beer);
        await _beerRepository.SaveAsync();

        return beer;
    }
}

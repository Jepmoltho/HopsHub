using HopsHub.Api.Data;
using HopsHub.Api.Interfaces;
using HopsHub.Api.Models;
using HopsHub.Api.Shared;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Services;

public class BeerService : IBeerService
{
    //private readonly BeerContext _beerContext;

    //public BeerService(BeerContext beerContext)
    //{
    //	_beerContext = beerContext;
    //}
    private readonly IRepository<Beer> _beerRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Models.Type> _typeRepository;
    private readonly IRepository<Rating> _ratingRepository;


    public BeerService(IRepository<Beer> beerRepository, IRepository<User> userRepository, IRepository<Models.Type> typeRepository, IRepository<Rating> ratingRepository)
    {
        _beerRepository = beerRepository;
        _userRepository = userRepository;
        _typeRepository = typeRepository;
        _ratingRepository = ratingRepository;
    }


    //public async Task<List<Beer>> GetBeers()
    //{
    //	return await _beerContext.Beers
    //           .Include(b => b.Type)
    //           .Include(b => b.Ratings)
    //           .ToListAsync();
    //   }
    public async Task<List<Beer>> GetBeers()
    {
        return await _beerRepository.GetQuerable()
            .Include(b => b.Type)
            .Include(b => b.Ratings)
            .ToListAsync();
    }

    public async Task<List<Rating>> GetRatings()
    {
        return await _ratingRepository.GetAllAsync();
    }

    public async Task<List<Models.Type>> GetTypes()
    {
        return await _typeRepository.GetAllAsync();
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepository.GetQuerable()
            .Include(u => u.Ratings)
            .ToListAsync();
        //return await _beerContext.Users
        //    .Include(u => u.Ratings)
        //    .ToListAsync();
    }

    public async Task<List<Beer>> GetBeersByType(int typeId)
    {
        var typeExist = await _typeRepository.GetByIdAsync(typeId);

        if (typeExist == null) {
            return new List<Beer>();
        }

        //To do: Fix nullable type
        var beers = await _beerRepository.GetQuerable()
                        .Include(b => b.Type)
                        .Where(b => b.Type.Id == typeId)
                        .ToListAsync();

        return beers;

        //var typeExist = await _beerContext.Types
        //            .Where(t => t.Id == typeId)
        //            .AnyAsync();

        //if (!typeExist)
        //{
        //    return new List<Beer>();
        //}

        ////To do: Fix nullable type
        //var beers = await _beerContext.Beers
        //                .Include(b => b.Type)
        //                .Where(b => b.Type.Id == typeId)
        //                .ToListAsync();

        //return beers;
    }

    public async Task<List<Rating>> GetRatingsByUser(Guid userId)
    {
        //var exist = await _beerContext.Users
        //            .AnyAsync(u => u.Id == userId);

        //if (!exist)
        //{
        //    return new List<Rating>();
        //}

        var ratings = await _ratingRepository.GetQuerable()
                                .Where(r => r.UserId == userId)
                                .Include(r => r.Beer)
                                .ToListAsync();

        return ratings;
    }

    public async Task<List<Rating>> GetRatingsByUserAndType(Guid userId, int typeId)
    {
        //var exist = await _beerContext.Users.AnyAsync(u => u.Id == userId) &&
        //            await _beerContext.Types.AnyAsync(t => t.Id == typeId);

        //if (!exist)
        //{
        //    return new List<Rating>();
        //}

        var ratings = await _ratingRepository.GetQuerable()
                        .Where(r => r.UserId == userId)
                        .Include(r => r.Beer)
                        .Where(r => r.Beer.TypeId == typeId)
                        .ToListAsync();

        return ratings;
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


using System;
using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Helpers;

public static class DTOHelper
{
    public static List<BeerDisplayDTO> MapBeersToBeerDisplayDTO(List<BeerDTO> beerDTOs)
    {
        return beerDTOs.
            OrderByDescending(beer => beer.AverageScore).
            Select((beer, index) => new BeerDisplayDTO
            {
                Name = beer.Name,
                Description = beer.Description,
                Score = beer.AverageScore,
                Alc = beer.Alc,
                TypeId = beer.TypeId,
                BrewerId = beer.BrewerId,
                Rank = index + 1

            })
            .OrderByDescending(beer => beer.Score)
            .ToList();
    }

    public static List<BeerDisplayDTO> MapRatingsToBeerDisplayDTO(List<RatingDTO> ratingDTOs)
    {
        var sortedBeers = ratingDTOs
            .OrderByDescending(rating => rating.Score)
            .Select((rating, index) => new BeerDisplayDTO
            {
                Name = rating.Beer.Name,
                Description = "N/A",
                Score = rating.Score,
                Alc = 0,
                TypeId = 0,
                BrewerId = 0,
                Rank = index + 1
            })
            .ToList();

        return sortedBeers;
    }
}


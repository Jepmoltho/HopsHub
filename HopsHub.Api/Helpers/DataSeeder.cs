using HopsHub.Api.Constants;
using HopsHub.Api.Models;
using HopsHub.Api.Repositories.Interfaces;

namespace HopsHub.Api.Helpers;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        await SeedTypes(serviceProvider);
        await SeedBrewers(serviceProvider);
        await SeedBeers(serviceProvider);
        await SeedRatings(serviceProvider);
    }

    public static async Task SeedTypes(IServiceProvider serviceProvider)
    {
        var typeRepository = serviceProvider.GetRequiredService<IRepository<Models.Type>>();

        var typesInTable = await typeRepository.CountAsync();
        if (typesInTable > 0) return;

        var typesToSeed = new List<Models.Type>()
        {
            new Models.Type { Name = TypeConstants.Pilsner },
            new Models.Type { Name = TypeConstants.IPA, ShortName = "IPA" },
            new Models.Type { Name = TypeConstants.Sour },
            new Models.Type { Name = TypeConstants.Lager },
            new Models.Type { Name = TypeConstants.Other },
            new Models.Type { Name = TypeConstants.Stout },
            new Models.Type { Name = TypeConstants.Porter },
            new Models.Type { Name = TypeConstants.WheatBeer },
            new Models.Type { Name = TypeConstants.AmberAle },
            new Models.Type { Name = TypeConstants.BelgianAle }
        };

        foreach (var type in typesToSeed)
        {
            await typeRepository.AddAsync(type);
        }

        await typeRepository.SaveAsync();
    }

    public static async Task SeedBrewers(IServiceProvider serviceProvider)
    {
        var brewerRepository = serviceProvider.GetRequiredService<IRepository<Brewer>>();

        var brewersInTable = await brewerRepository.CountAsync();
        if (brewersInTable > 0) return;

        var brewersToSeed = new List<Brewer>()
        {
            new Brewer { Name = "Best Brewer" },
            new Brewer { Name = "Tuborg" },
            new Brewer { Name = "Carlsberg" },
            new Brewer { Name = "Mikkeller" },
            new Brewer { Name = "Guinness" }
        };

        foreach (var brewer in brewersToSeed)
        {
            await brewerRepository.AddAsync(brewer);
        }

        await brewerRepository.SaveAsync();
    }

    public static async Task SeedBeers(IServiceProvider serviceProvider)
    {
        var beerRepository = serviceProvider.GetRequiredService<IRepository<Beer>>();

        var beersInTable = await beerRepository.CountAsync();
        if (beersInTable > 0)
        {
            return;
        }

        var beersToSeed = new List<Beer>()
        {
            new Beer { Name = "Jacobsen Yakima IPA", Alc = 6.5M, TypeId = 3, BrewerId = 1, CreatedByUser = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80") },
            new Beer { Name = "Budweiser", Alc = 5.0M, TypeId = 4, BrewerId = 1, CreatedByUser = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80")  },
            new Beer { Name = "Flamingo Sour", Alc = 4.2M, TypeId = 3, BrewerId = 1, CreatedByUser = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80")  },
            new Beer { Name = "Zen IPA", Alc = 6.5M, TypeId = 2, BrewerId = 1, CreatedByUser = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80")  },
            new Beer { Name = "Tuborg Pilsner", Alc = 4.6M, TypeId = 1, BrewerId = 2, CreatedByUser = new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8") },
            new Beer { Name = "Guinness Draught", Alc = 4.6M, TypeId = 7, BrewerId = 5, CreatedByUser = new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8") }
        };

        foreach (var beer in beersToSeed)
        {
            await beerRepository.AddAsync(beer);
        }

        await beerRepository.SaveAsync();
    }

    public static async Task SeedRatings(IServiceProvider serviceProvider)
    {
        var ratingRepository = serviceProvider.GetRequiredService<IRepository<Rating>>();

        var ratingsInTable = await ratingRepository.CountAsync();
        if (ratingsInTable > 0) return;

        var ratingsToSeed = new List<Rating>()
        {
            new Rating { BeerId = 1, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "Nice and bitter IPA" },
            new Rating { BeerId = 2, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "Heavy and dark Lager" },
            new Rating { BeerId = 3, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "So sour it made my eyes squint" },
            new Rating { BeerId = 6, UserId = new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8"), Comment = "Black as the night" },
            new Rating { BeerId = 6, UserId = new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8"), Comment = "Not very good" }
        };

        foreach (var rating in ratingsToSeed)
        {
            await ratingRepository.AddAsync(rating);
        }

        await ratingRepository.SaveAsync();
    }
}

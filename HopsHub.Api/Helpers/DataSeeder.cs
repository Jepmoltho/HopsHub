using HopsHub.Api.Constants;
using HopsHub.Api.Models;
using HopsHub.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HopsHub.Api.Helpers;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider, string testUserPassword)
    {
        await SeedRoles(serviceProvider);
        await SeedUsers(serviceProvider, testUserPassword);
        await SeedTypes(serviceProvider);
        await SeedBrewers(serviceProvider);
        await SeedBeers(serviceProvider);
        await SeedRatings(serviceProvider);
    }

    //1: Roles
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "User" });
        }

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin" });
        }
    }

    //2: Users
    public static async Task SeedUsers(IServiceProvider serviceProvider, string testUserPassword)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        foreach (var user in TestUserConstants.TestUsers)
        {
            if (user.Email != null && await userManager.FindByEmailAsync(user.Email) == null)
            {
                await CreateTestUser(userManager, user, testUserPassword);
            }
        }
    }

    private static async Task CreateTestUser(UserManager<User> userManager, User user, string testUserPassword)
    {
        var result = await userManager.CreateAsync(user, testUserPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }
    }

    //3: Types 
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

    //4: Brewers
    public static async Task SeedBrewers(IServiceProvider serviceProvider)
    {
        var brewerRepository = serviceProvider.GetRequiredService<IRepository<Brewer>>();

        var brewersInTable = await brewerRepository.CountAsync();
        if (brewersInTable > 0) return;

        var brewersToSeed = new List<Brewer>()
        {
            new Brewer { Name = "Best Brewer" },
            new Brewer { Name = "The Beer Company" },
            new Brewer { Name = "Brewit" },
            new Brewer { Name = "HopsHouse" },
            new Brewer { Name = "PaleAf" }
        };

        foreach (var brewer in brewersToSeed)
        {
            await brewerRepository.AddAsync(brewer);
        }

        await brewerRepository.SaveAsync();
    }

    //5: Beers
    public static async Task SeedBeers(IServiceProvider serviceProvider)
    {
        var beerRepository = serviceProvider.GetRequiredService<IRepository<Beer>>();
        var userRepository = serviceProvider.GetRequiredService<IRepository<User>>();

        var beersInTable = await beerRepository.CountAsync();
        if (beersInTable > 0)
        {
            return;
        }

        var users = await userRepository.GetAllAsync();
        var getNextUserId = CreateUserIdEnumerator(users);

        var beersToSeed = new List<Beer>()
        {
            new Beer { Name = "Pale Sensation", Alc = 6.5M, TypeId = 3, BrewerId = 1, CreatedByUser = getNextUserId() },
            new Beer { Name = "Pilsner", Alc = 5.0M, TypeId = 4, BrewerId = 1, CreatedByUser = getNextUserId()  },
            new Beer { Name = "Flamingo Sour", Alc = 4.2M, TypeId = 3, BrewerId = 1, CreatedByUser = getNextUserId() },
            new Beer { Name = "Zen IPA", Alc = 6.5M, TypeId = 2, BrewerId = 1, CreatedByUser = getNextUserId() },
            new Beer { Name = "Pistol Pilsner", Alc = 4.6M, TypeId = 1, BrewerId = 2, CreatedByUser = getNextUserId() },
            new Beer { Name = "Wet and Dark", Alc = 4.6M, TypeId = 7, BrewerId = 5, CreatedByUser = getNextUserId() }
        };

        foreach (var beer in beersToSeed)
        {
            await beerRepository.AddAsync(beer);
        }

        await beerRepository.SaveAsync();
    }

    //6: Ratings
    public static async Task SeedRatings(IServiceProvider serviceProvider)
    {
        var ratingRepository = serviceProvider.GetRequiredService<IRepository<Rating>>();
        var userRepository = serviceProvider.GetRequiredService<IRepository<User>>();
        var beerRepository = serviceProvider.GetRequiredService<IRepository<Beer>>();

        var ratingsInTable = await ratingRepository.CountAsync();
        if (ratingsInTable > 0) return;

        var usersInTable = await userRepository.CountAsync();
        if (usersInTable == 0) return;

        var users = await userRepository.GetAllAsync();
        var beers = await beerRepository.GetAllAsync();

        var getNextUserId = CreateUserIdEnumerator(users);
        var getNextBeerId = CreateBeerIdEnumerator(beers);

        var ratingsToSeed = new List<Rating>()
        {
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=7.0M, Comment = "Very nice!" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=8.2M, Comment = "A sensation beer" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=4.3M, Comment = "Standard but has a bitter aftertaste that is too much" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=6.0M, Comment = "Perfectly carbonated" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=2.1M, Comment = "This is absolutely trash" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=5.3M, Comment = "Good to serve at a BBQ" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=5.0M, Comment = "Good not great" },
            new Rating { BeerId = getNextBeerId(), UserId = getNextUserId(), Score=10M, Comment = "Probably the best beer I have ever had" },
        };

        foreach (var rating in ratingsToSeed)
        {
            await ratingRepository.AddAsync(rating);
        }

        await ratingRepository.SaveAsync();
    }

    //Helper function to get different user IDs 
    public static Func<Guid> CreateUserIdEnumerator(IEnumerable<User> users)
    {
        var userEnumerator = users.GetEnumerator();

        Guid GetNextUserId()
        {
            if (!userEnumerator.MoveNext())
            {
                userEnumerator.Reset();
                userEnumerator.MoveNext();
            }
            return userEnumerator.Current.Id;
        }

        return GetNextUserId;
    }

    //Helper function to get different beer IDs 
    public static Func<int> CreateBeerIdEnumerator(IEnumerable<Beer> beers)
    {
        var beerEnumerator = beers.GetEnumerator();

        int GetNextBeerId()
        {
            if (!beerEnumerator.MoveNext())
            {
                beerEnumerator.Reset();
                beerEnumerator.MoveNext();
            }
            return beerEnumerator.Current.Id;
        }

        return GetNextBeerId;
    }

    //Todo: See if you can make the enumerator helper method generic
    //public static Func<T> CreateUserIdEnumerator<T>(IEnumerable<T> entities)
    //{
    //    var enumerator = entities.GetEnumerator();

    //    Guid GetNextId()
    //    {
    //        if (!enumerator.MoveNext())
    //        {
    //            enumerator.Reset();
    //            enumerator.MoveNext();
    //        }
    //        return enumerator.Current.Id; .Id;
    //    }

    //    return GetNextId;
    //}
}

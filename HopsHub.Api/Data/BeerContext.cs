using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HopsHub.Api.Data;

public class BeerContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> //DbContext
{
    public BeerContext(DbContextOptions<BeerContext> options) : base(options) { }

    public DbSet<Beer> Beers { get; set; }
    public DbSet<Models.Type> Types { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Brewer> Brewers { get; set; }

    //Defines pipeline for creating relationships and seeding testdata 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // A Beer has exactly one type and a Type can have many Beer
        modelBuilder.Entity<Beer>()
            .HasOne(b => b.Type);

        // A Beer has one to many Ratings and a Rating has excatly one Beer
        modelBuilder.Entity<Beer>()
            .HasMany(b => b.Ratings);

        //A user has zero to many ratings
        modelBuilder.Entity<User>()
            .HasMany(u => u.Ratings);

        //A beer has excatly one brewer
        modelBuilder.Entity<Beer>()
            .HasOne(b => b.Brewer);

        //To do: Investigate how to setup foreign key constraints such that you can't cascade delete entities. Deleting a beer with sql should not automatically delete its rating. If a beer has a rating, you have do delete the rating before you can delete a beer using sql or calling endpoint. Same constraints as with your clients database, where you couldnt delete an entity with mandatory related entities, before the related entity was deleted first.  

        //To do: Implement soft delete. You dont implement any actual delete operations.

        //To do: Write a script that empties your tables and refills them with speciefied test data on the dev environment each time the application runs. (Powershell script?)

        // Seed test data for Types
        //modelBuilder.Entity<Models.Type>().HasData(
        //    new Models.Type { Id = 1, Name = TypeConstants.Pilsner },
        //    new Models.Type { Id = 2, Name = TypeConstants.IPA, ShortName = "IPA" },
        //    new Models.Type { Id = 3, Name = TypeConstants.Sour },
        //    new Models.Type { Id = 4, Name = TypeConstants.Lager },
        //    new Models.Type { Id = 5, Name = TypeConstants.Other },
        //    new Models.Type { Id = 6, Name = TypeConstants.Stout },
        //    new Models.Type { Id = 7, Name = TypeConstants.Porter },
        //    new Models.Type { Id = 8, Name = TypeConstants.WheatBeer },
        //    new Models.Type { Id = 9, Name = TypeConstants.AmberAle },
        //    new Models.Type { Id = 10, Name = TypeConstants.BelgianAle }
        //);

        //modelBuilder.Entity<Brewer>().HasData(
        //    new Brewer { Id = 1, Name = "Test Brewer" },
        //    new Brewer { Id = 2, Name = "Tuborg" },
        //    new Brewer { Id = 3, Name = "Carlsberg" },
        //    new Brewer { Id = 4, Name = "Mikkeller" },
        //    new Brewer { Id = 5, Name = "Guinness" }
        //);

        // Seed test data for Beers
        //modelBuilder.Entity<Beer>().HasData(
        //    new Beer { Id = 1, Name = "Sample IPA", Alc = 6.5M, TypeId = 2, BrewerId = 1 },
        //    new Beer { Id = 2, Name = "Crispy Lager", Alc = 5.0M, TypeId = 4, BrewerId = 1 },
        //    new Beer { Id = 3, Name = "Tart Sour", Alc = 4.2M, TypeId = 3, BrewerId = 1 },
        //    new Beer { Id = 4, Name = "Other IPA", Alc = 6.5M, TypeId = 2, BrewerId = 1 },
        //    new Beer { Id = 5, Name = "Tuborg Pilsner", Alc = 4.6M, TypeId = 1, BrewerId = 2 },
        //    new Beer { Id = 6, Name = "Guinness Draught", Alc = 4.6M, TypeId = 7, BrewerId = 5 }
        //);

        //Seed test data for Ratings
        //modelBuilder.Entity<Rating>().HasData(
        //    new Rating { Id = 1, BeerId = 1, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "Nice and bitter IPA" },
        //    new Rating { Id = 2, BeerId = 2, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "Heavy and dark Lager" },
        //    new Rating { Id = 3, BeerId = 3, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "So sour it made my eyes squint" },
        //    new Rating { Id = 4, BeerId = 6, UserId = new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8"), Comment = "Black as the night" },
        //    new Rating { Id = 5, BeerId = 6, UserId = new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8"), Comment = "Not very good" }
        //    );
    }
}

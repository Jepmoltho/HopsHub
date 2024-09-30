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
    //public DbSet<User> Users { get; set; }

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

        // Seed test data for Types
        modelBuilder.Entity<Models.Type>().HasData(
            new Models.Type { Id = 1, Name = "Pilsner" },
            new Models.Type { Id = 2, Name = "India Pale Ale", ShortName = "IPA" },
            new Models.Type { Id = 3, Name = "Sour" },
            new Models.Type { Id = 4, Name = "Lager" },
            new Models.Type { Id = 5, Name = "Other" }
        );

        // Seed test data for Beers
        modelBuilder.Entity<Beer>().HasData(
            new Beer { Id = 1, Name = "Sample IPA", Alc = 6.5M, TypeId = 2 },
            new Beer { Id = 2, Name = "Crispy Lager", Alc = 5.0M, TypeId = 4 },
            new Beer { Id = 3, Name = "Tart Sour", Alc = 4.2M, TypeId = 3 },
            new Beer { Id = 4, Name = "Other IPA", Alc = 6.5M, TypeId = 2 }
        );

        //Seed test data for Ratings
        modelBuilder.Entity<Rating>().HasData(
            new Rating { Id = 1, BeerId = 1, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "Nice and bitter IPA" },
            new Rating { Id = 2, BeerId = 2, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "Heavy and dark Lager" },
            new Rating { Id = 3, BeerId = 3, UserId = new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"), Comment = "So sour it made my eyes squint" }
        );
    }
}

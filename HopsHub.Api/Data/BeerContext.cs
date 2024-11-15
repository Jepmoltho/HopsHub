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

        //Todo: Investigate how to setup foreign key constraints such that you can't cascade delete entities. Deleting a beer with sql should not automatically delete its rating. If a beer has a rating, you have do delete the rating before you can delete a beer using sql or calling endpoint. Same constraints as with your clients database, where you couldnt delete an entity with mandatory related entities, before the related entity was deleted first.  

        //Todo: Implement soft delete. You dont implement any actual delete operations.

        //Todo: Write a script that empties your tables and refills them with speciefied test data on the dev environment each time the application runs. (Powershell script?)
    }
}

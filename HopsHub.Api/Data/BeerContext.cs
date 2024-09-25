using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Models;

namespace HopsHub.Api.Data;

public class BeerContext : DbContext
{
    public BeerContext(DbContextOptions<BeerContext> options) : base(options) { }

    public DbSet<Beer> Beers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Beer>().HasData(
            new Beer { Id = 1, Name = "Sample IPA", Alc = 6.5M },
            new Beer { Id = 2, Name = "Crispy Lager", Alc = 5.0M },
            new Beer { Id = 3, Name = "Tart Sour", Alc = 4.2M }
        );
    }
}


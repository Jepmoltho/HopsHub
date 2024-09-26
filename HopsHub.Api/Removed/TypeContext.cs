//using Microsoft.EntityFrameworkCore;
//using HopsHub.Api.Models;

//namespace HopsHub.Api.Data;

//public class TypeContext : DbContext
//{
//    public TypeContext(DbContextOptions<TypeContext> options) : base(options) { }

//    public DbSet<Models.Type> Types { get; set; }

    //Defines pipeline for creating relationships and seeding testdata 
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    // Define the relationship: One Type can have many Beers, and a Beer can have one Type
    //    //modelBuilder.Entity<Beer>()
    //    .HasOne(b => b.Type)
    //    .WithMany(t => t.Beers)
    //    .HasForeignKey(b => b.TypeId);

    //    // Seed test data for Types
    //    modelBuilder.Entity<Models.Type>().HasData(
    //        new Models.Type { Id = 1, Name = "Pilsner" },
    //        new Models.Type { Id = 2, Name = "India Pale Ale", ShortName = "IPA" },
    //        new Models.Type { Id = 3, Name = "Sour" },
    //        new Models.Type { Id = 4, Name = "Lager" },
    //        new Models.Type { Id = 5, Name = "Other" }
    //    );

    //    //Seed test data for Beers
    //    modelBuilder.Entity<Beer>().HasData(
    //        new Beer { Id = 1, Name = "Sample IPA", Alc = 6.5M, TypeId = 2 },
    //        new Beer { Id = 2, Name = "Crispy Lager", Alc = 5.0M, TypeId = 4 },
    //        new Beer { Id = 3, Name = "Tart Sour", Alc = 4.2M, TypeId = 3 }
    //    );
    // }
//}

using System;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Models;

namespace HopsHub.Api.Data;

public class BeerContext : DbContext
{
    public BeerContext(DbContextOptions<BeerContext> options) : base(options) { }

    public DbSet<Beer> Beers { get; set; }
}


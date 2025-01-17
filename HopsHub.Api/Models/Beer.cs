﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopsHub.Api.Models;

public class Beer
{
    public int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public required int TypeId { get; set; }

    public required int BrewerId { get; set; }

    public required Guid CreatedByUser { get; set; } 

    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.0, 100.0, ErrorMessage = "Alcohol percentage must be between 0 and 100")]
    public decimal Alc { get; set; } = 0;

    [Column(TypeName = "decimal(4, 2)")]
    public decimal AverageScore { get; set; } = 0;

    public Brewer? Brewer { get; set; }

    public Type? Type { get; set; } 

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public bool Deleted { get; set; } = false;
}


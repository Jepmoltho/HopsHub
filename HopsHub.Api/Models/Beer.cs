using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopsHub.Api.Models;

public class Beer
{
    public int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public required int BrewerId { get; set; }

    //To do: Fix nullable type
    public Brewer? Brewer { get; set; }

    //To do: Add relation from User to Beer as PostedBy Guid. Relation as Guid, not just a column. 

    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.0, 100.0, ErrorMessage = "Alcohol percentage must be between 0 and 100")]
    public decimal Alc { get; set; } = 0;

    [Column(TypeName = "decimal(3, 2)")]
    public decimal AverageScore { get; set; } = 0;

    public int TypeId { get; set; }

    //To do: Fix nullable type
    public Type? Type { get; set; } 

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}


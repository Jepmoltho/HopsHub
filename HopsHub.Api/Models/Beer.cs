using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using HopsHub.Api.Constants;

namespace HopsHub.Api.Models;

public class Beer
{
    public int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Alc { get; set; } = 0;

    [Column(TypeName = "decimal(3, 2)")]
    public decimal AverageScore { get; set; } = 0;

    public int TypeId { get; set; }

    //To do: Fix nullable type
    public Type? Type { get; set; } 

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}


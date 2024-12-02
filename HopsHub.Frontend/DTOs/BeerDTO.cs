using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Changed 
public class BeerDTO
{
    public required string Name { get; set; }

    public string Description { get; set; } = "";

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.0, 100.0, ErrorMessage = "Alcohol percentage must be between 0 and 100")]
    public decimal Alc { get; set; } = 0;

    public required int TypeId { get; set; }

    public required int BrewerId { get; set; }

    public Guid UserId { get; set; } = Guid.Empty;

    //Added
    public required decimal AverageScore { get; set; } = 0.0M;
}

public class UpdateBeerDTO
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = "";

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.0, 100.0, ErrorMessage = "Alcohol percentage must be between 0 and 100")]
    public decimal Alc { get; set; } = 0;

    public required int TypeId { get; set; }

    public required int BrewerId { get; set; }
}

public class DeleteBeerDTO
{
    public int Id { get; set; }

    public required bool Deleted { get; set; }
}


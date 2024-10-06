namespace HopsHub.Api.DTOs;

public class BeerDTO
{
    public required string Name { get; set; }
    public string Description { get; set; } = "";
    public decimal Alc { get; set; } = 0;
    public required int TypeId { get; set; }
    public required int BrewerId { get; set; }
}


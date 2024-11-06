namespace HopsHub.Api.DTOs;

public class UpdateBrewerDTO
{
    public required int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}



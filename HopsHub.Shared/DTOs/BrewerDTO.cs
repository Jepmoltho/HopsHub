namespace HopsHub.Shared.DTOs;

public class BrewerDTO
{
    public required string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}

public class UpdateBrewerDTO
{
    public required int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}

public class DeleteBrewerDTO
{
    public required int Id { get; set; }

    public required bool Deleted { get; set; }
}

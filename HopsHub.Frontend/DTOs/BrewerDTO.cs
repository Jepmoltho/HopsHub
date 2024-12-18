namespace HopsHub.Shared.DTOs;

//Changed
public class BrewerDTO
{
    public required int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}

//Added
public class AddBrewerDTO
{
    public required string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}


//Added
public class SelectBrewerDTO
{
    public required int Id { get; set; } = 0;

    public required string Name { get; set; }
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

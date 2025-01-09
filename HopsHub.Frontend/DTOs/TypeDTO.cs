namespace HopsHub.Shared.DTOs;

//Changed
public class TypeDTO
{
    public required int Id { get; set; } = 0;

    public required string Name { get; set; }

    public required string ShortName { get; set; } = "";

    public string Link { get; set; } = "";
}

//Added
public class SelectTypeDTO
{
    public required int Id { get; set; } = 0;

    public required string Name { get; set; }
}

public class UpdateTypeDTO
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required string ShortName { get; set; } = "";
}

public class DeleteTypeDTO
{
    public required int Id { get; set; }

    public required bool Deleted = false;
}
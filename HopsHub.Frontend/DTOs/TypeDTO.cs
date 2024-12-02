namespace HopsHub.Shared.DTOs;

//Changed
public class TypeDTO
{
    //Added
    public required int Id { get; set; } = 0;

    public required string Name { get; set; }

    public required string ShortName { get; set; } = "";
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
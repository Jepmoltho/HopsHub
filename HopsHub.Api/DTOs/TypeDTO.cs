namespace HopsHub.Shared.DTOs;

public class TypeDTO
{
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
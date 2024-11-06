namespace HopsHub.Api.DTOs;

public class UpdateTypeDTO
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required string ShortName { get; set; } = "";
}
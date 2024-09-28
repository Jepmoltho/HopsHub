namespace HopsHub.Api.Models;

public class Type
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string ShortName { get; set; } = "";
}
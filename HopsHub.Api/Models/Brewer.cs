namespace HopsHub.Api.Models;

public class Brewer
{
    public int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public bool Deleted { get; set; } = false;
}


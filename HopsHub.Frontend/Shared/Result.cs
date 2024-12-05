namespace HopsHub.Shared;

public class Result
{
    public bool Succeeded { get; set; }

    public required string Message { get; set; }

    public string Token { get; set; }
}


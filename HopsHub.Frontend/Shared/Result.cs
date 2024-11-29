namespace HopsHub.Shared;
//To do: Publish shared models as nuget package 

public class Result
{
    public bool Succeeded { get; set; }

    public required string Message { get; set; }
}


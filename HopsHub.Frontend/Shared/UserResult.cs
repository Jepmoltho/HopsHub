namespace HopsHub.Shared;
//To do: Publish shared models as nuget package 

public class UserResult : Result
{
    public Guid? UserId { get; set; } 

    public string Token { get; set; } = "";
}


namespace HopsHub.Shared;
//Todo: Publish shared models as nuget package 

public class UserResult : Result
{
    public Guid? UserId { get; set; }

    //Todo: Delete property: Token is used in LoginResult
    public string Token { get; set; } = "";
}


namespace HopsHub.Api.Shared;

public class UserResult : Result
{
    public Guid? UserId { get; set; } 

    public string Token { get; set; } = "";
}


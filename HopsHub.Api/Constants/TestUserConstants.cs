using HopsHub.Api.Models;
namespace HopsHub.Api.Constants;

public static class TestUserConstants
{
    public static readonly List<User> TestUsers = new List<User> {
        new User
        {
            UserName = "user1",
            Email = "user1@test.com",
            Id = Guid.NewGuid()
        },
        new User
        {
            UserName = "user2",
            Email = "user2@test.com",
            Id = Guid.NewGuid()
        },
        new User
        {
            UserName = "user3",
            Email = "user3@test.com",
            Id = Guid.NewGuid()
        }
    };
}
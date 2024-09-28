using Microsoft.AspNetCore.Identity;

namespace HopsHub.Api.Models;

public class User : IdentityUser<Guid>
{
    //public Guid Id { get; set; }

    //public required string UserName { get; set; }

    //public required string Email { get; set; }

    //public required string PasswordHash { get; set; }

    //public required string PasswordSalt { get; set; }

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}


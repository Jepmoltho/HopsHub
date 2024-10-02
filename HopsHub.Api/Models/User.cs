using Microsoft.AspNetCore.Identity;

namespace HopsHub.Api.Models;

public class User : IdentityUser<Guid>
{
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}


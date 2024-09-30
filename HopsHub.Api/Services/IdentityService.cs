using HopsHub.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace HopsHub.Api.Services;

public class IdentityService
{
    public static async Task SeedUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        // Ensure an admin test role exists
        //if (!await roleManager.RoleExistsAsync("Admin"))
        //{
        //    await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin" });
        //}

        // Ensure a user test role exists
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "User" });
        }

        // Ensure a test user exists
        if (await userManager.FindByEmailAsync("user1@test.dk") == null)
        {
            var user = new User
            {
                UserName = "user1",
                Email = "user1@test.dk",
                Id = Guid.NewGuid()
            };

            var result = await userManager.CreateAsync(user, "Test@1234"); // Creates user and hashes the password

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User"); // Optionally assign a role
            }
        }
    }
}


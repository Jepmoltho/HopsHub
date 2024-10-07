using Microsoft.AspNetCore.Identity;
using HopsHub.Api.Constants;

namespace HopsHub.Api.Services;

public static class IdentityService
{
    public static async Task SeedUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<Models.User>>();

        foreach (var user in TestUserConstants.TestUsers)
        {
            if (user.Email != null && await userManager.FindByEmailAsync(user.Email) == null)
            {
                await CreateTestUser(userManager, user);
            }
        }
    }

    private static async Task CreateTestUser(UserManager<Models.User> userManager, Models.User user)
    {
        var result = await userManager.CreateAsync(user, "userPassw0rd!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }
    }

    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "User" });
        }

        if (!await roleManager.RoleExistsAsync("Admin")) {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin" });
        }
    }
}


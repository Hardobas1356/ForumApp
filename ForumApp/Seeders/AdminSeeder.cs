using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ForumApp.Web.Seeders;

public class AdminSeeder
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole<Guid>> roleManager;

    public AdminSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    private const string UserName = "admin";
    private const string DisplayName = "Administrator";
    private const string AdminEmail = "admin@admin.com";
    private const string AdminPassword = "Admin123!";
    private const string AdminRole = "Admin";

    public async Task SeedAsync()
    {
        if (!await roleManager.RoleExistsAsync(AdminRole))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(AdminRole));
        }

        ApplicationUser? adminUser = await userManager.FindByEmailAsync(AdminEmail);
        if (adminUser == null)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = UserName,
                DisplayName = DisplayName,
                Email = AdminEmail,
                EmailConfirmed = true
            };

            IdentityResult result = await userManager.CreateAsync(user, AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, AdminRole);
            }
            else
            {
                throw new Exception("Failed to create admin user:\n" +
                    string.Join("\n", result.Errors.Select(e => e.Description)));
            }
        }
        else if (!await userManager.IsInRoleAsync(adminUser, AdminRole))
        {
            await userManager.AddToRoleAsync(adminUser, AdminRole);
        }
    }
}

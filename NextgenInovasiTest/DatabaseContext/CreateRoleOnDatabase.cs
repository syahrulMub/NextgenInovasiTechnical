using Microsoft.AspNetCore.Identity;

namespace NextgenInovasiTest.DatabaseContext;

public static class CreateRoleOnDatabase
{
    public static async void CreateRole(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole>>();

        await SetRolesAsync(roleManager);
    }
    static async Task SetRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "Employee" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

}


using Microsoft.AspNetCore.Identity;
using NextgenInovasiTest.Models;

namespace NextgenInovasiTest.DatabaseContext;

public class CreateAdminOnDatabase
{
    public static async void CreateAdminDataOnDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await SetAdminAccount(userManager);
    }
    static async Task SetAdminAccount(UserManager<User> userManager)
    {
        //membaut instance admin ke databse. permanen set
        var adminEmail = "admin@nextgen.com";
        var adminPassword = "Admin123@";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new User
            {
                Email = adminEmail,
                UserName = adminEmail,
                PhoneNumber = "081115673",
                EmailConfirmed = true

            };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

    }
}
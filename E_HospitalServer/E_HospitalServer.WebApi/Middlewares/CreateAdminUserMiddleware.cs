using E_HospitalServer.Entities.Enums;
using E_HospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace E_HospitalServer.WebApi.Middlewares;

public static class CreateAdminUserMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
        if (userManager.Users.Any(x => x.UserName == "admin")) return;
        var user = new User
        {
            UserName = "admin",
            Email = "admin@admin.com",
            EmailConfirmed = true,
            FirstName = "Gani",
            LastName = "Ozturk",
            IdentityNumber = "11111111111",
            FullAddress = "Tekirdag",
            IsActive = true,
            IsDeleted = false,
            DateOfBirth = DateOnly.Parse("24.01.1997"),
            BloodType = "AB rh+",
            UserType = UserType.Admin
        };
        userManager.CreateAsync(user, "1").Wait();
    }
}
using System.Net;
using E_HospitalServer.Business.Services;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Models;
using E_HostpitalServer.Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_HospitalServer.DataAccess.Services;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager) : IAuthService
{
    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var emailOrUsername = request.EmailOrUsername.ToUpper();
        var user = await userManager.Users
            .FirstOrDefaultAsync(x =>
                x.NormalizedEmail == emailOrUsername ||
                x.NormalizedUserName == emailOrUsername,
                cancellationToken);
        if (user is null)
        {
            return (500, "User not found");
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            if (timeSpan is not null)
                return (500, $"Your user has been locked for {Math.Ceiling(timeSpan.Value.TotalMinutes)} minutes due to entering the wrong password 3 times.");
            else
                return (500, "Your user has been locked out for 5 minutes due to entering the wrong password 3 times.");
        }

        if (signInResult.IsNotAllowed)
        {
            return (500, "Your e-mail address is not confirmed");
        }

        if (!signInResult.Succeeded)
        {
            return (500, "Your password is wrong");
        }

        return new LoginResponseDto(
            "token",
            "refreshtoken",
            DateTime.Now.AddDays(1));
    }
}
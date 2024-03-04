using E_HospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_HospitalServer.DataAccess.Extensions;

public static class UserManagerExtensions
{
    public static async Task<User?> FindByIdentityNumber(this UserManager<User> userManager, string identityNumber)
    {
        return await userManager.Users.FirstOrDefaultAsync(p=> p.IdentityNumber == identityNumber);
    }
}
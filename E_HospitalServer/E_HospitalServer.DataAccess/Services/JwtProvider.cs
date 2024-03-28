using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_HospitalServer.DataAccess.Options;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_HospitalServer.DataAccess.Services;

public class JwtProvider(
    UserManager<User> userManager,
    IOptions<JwtOptions> jwtOptions)
{
    public async Task<LoginResponseDto> CreateToken(User user, bool rememberMe)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
            new Claim("UserType", user.UserType.ToString())
        };
        
        DateTime expires = DateTime.UtcNow.AddHours(1);

        if (rememberMe)
        {
            expires = expires.AddDays(1);
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        string refreshToken = Guid.NewGuid().ToString();
        DateTime refreshTokenExpires = expires.AddHours(1);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        await userManager.UpdateAsync(user);

        return new(token, refreshToken, refreshTokenExpires);
    }
}
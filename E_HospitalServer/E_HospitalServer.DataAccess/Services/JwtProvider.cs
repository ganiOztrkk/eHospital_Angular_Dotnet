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
    public async Task<LoginResponseDto> CreateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, string.Join(" ",user.FirstName, user.LastName)),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("Username", user.UserName ?? "" )
            
        };
        
        var expires = DateTime.UtcNow.AddHours(4);
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        
        JwtSecurityToken jwtSecurityToken = new(
            issuer:jwtOptions.Value.Issuer,
            audience:jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            );

        JwtSecurityTokenHandler handler = new();

        var token = handler.WriteToken(jwtSecurityToken);

        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenExpires = expires.AddHours(1);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        await userManager.UpdateAsync(user);

        return new(token, refreshToken, refreshTokenExpires);
    }
}
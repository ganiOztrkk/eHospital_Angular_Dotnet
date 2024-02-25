using E_HospitalServer.Entities.Enums;

namespace E_HospitalServer.Entities.DTOs;

public sealed record LoginResponseDto(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires
    );
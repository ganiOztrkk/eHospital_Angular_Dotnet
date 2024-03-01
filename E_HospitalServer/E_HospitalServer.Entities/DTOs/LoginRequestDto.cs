namespace E_HospitalServer.Entities.DTOs;

public sealed record LoginRequestDto(
    string EmailOrUsername,
    string Password,
    bool RememberMe = false
);
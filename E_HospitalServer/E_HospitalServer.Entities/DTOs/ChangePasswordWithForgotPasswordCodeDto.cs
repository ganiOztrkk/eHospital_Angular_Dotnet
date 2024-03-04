namespace E_HospitalServer.Entities.DTOs;

public sealed record ChangePasswordWithForgotPasswordCodeDto(
    int ForgotPasswordCode,
    string NewPassword
    );
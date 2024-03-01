using E_HospitalServer.Entities.Enums;

namespace E_HospitalServer.Entities.DTOs;

public sealed record CreateUserDto(
    string FirstName,
    string LastName,
    string IdentityNumber = "11111111111",
    string FullAddress = "",
    string? Email = null,
    string? Username = null,
    string? Password = null,
    string? PhoneNumber = null,
    DateOnly? DateOfBirth = null,
    string? BloodType = null,
    Specialty? Specialty = null,
    List<string>? WorkingDays = null
    );
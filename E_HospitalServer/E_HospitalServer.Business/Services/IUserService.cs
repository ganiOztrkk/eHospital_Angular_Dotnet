using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Models;
using E_HostpitalServer.Core.Utilities.Results;

namespace E_HospitalServer.Business.Services;

public interface IUserService
{
    Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<User>> FindPatientWithIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken);
    Task<Result<string>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken);
    Task<Result<List<User>>> GetAllDoctorsAsync(CancellationToken cancellationToken);
}


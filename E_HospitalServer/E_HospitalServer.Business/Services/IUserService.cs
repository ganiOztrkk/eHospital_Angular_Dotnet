using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Models;
using E_HostpitalServer.Core.Utilities.Results;

namespace E_HospitalServer.Business.Services;

public interface IUserService
{
    Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<Guid>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken);
}


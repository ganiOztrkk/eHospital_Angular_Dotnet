using E_HospitalServer.Entities.DTOs;
using E_HostpitalServer.Core.Utilities.Results;

namespace E_HospitalServer.Business.Services;

public interface IAuthService
{
     Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
     
     Task<Result<LoginResponseDto>> GetTokeByRefreshToken(string refreshToken, CancellationToken cancellationToken);
     
}
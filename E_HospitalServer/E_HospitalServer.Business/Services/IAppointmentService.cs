using E_HospitalServer.Entities.DTOs;
using E_HostpitalServer.Core.Utilities.Results;

namespace E_HospitalServer.Business.Services;

public interface IAppointmentService
{
    Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto request, CancellationToken cancellationToken);
}
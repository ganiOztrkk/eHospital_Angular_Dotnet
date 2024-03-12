using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Models;
using E_HostpitalServer.Core.Utilities.Results;

namespace E_HospitalServer.Business.Services;

public interface IAppointmentService
{
    Task<Result<string>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken);
    Task<Result<string>> CompleteAsync(CompleteAppointmentDto request,CancellationToken cancellationToken);
    Task<Result<List<Appointment>>> GetAllByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken);
}
namespace E_HospitalServer.Entities.DTOs;

public sealed record CreateAppointmentDto(
    Guid DoctorId,
    Guid PatientId,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price
);
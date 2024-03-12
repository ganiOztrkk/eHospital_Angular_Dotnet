namespace E_HospitalServer.Entities.DTOs;

public sealed record CompleteAppointmentDto(
    Guid AppointmentId,
    string EpicrisisReport
    );
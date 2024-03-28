using E_HospitalServer.Entities.Enums;

namespace E_HospitalServer.Entities.Models;

public sealed class DoctorDetail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Specialty Specialty { get; set; } = Specialty.Acil;
    public List<string> WorkingDays { get; set; } = new();
    public decimal AppointmentPrice { get; set; }
    public string SpecialtyName => Specialty.ToString();
}
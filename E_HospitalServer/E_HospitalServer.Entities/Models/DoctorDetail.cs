namespace E_HospitalServer.Entities.Models;

public sealed class DoctorDetail
{
    public Guid UserId { get; set; }
    public string Specialty { get; set; }
    public List<string> WorkingDays { get; set; }
}
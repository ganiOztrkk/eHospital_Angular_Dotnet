using E_HospitalServer.DataAccess.Context;
using E_HospitalServer.Entities.Models;
using E_HospitalServer.Entities.Repositories;
using GenericRepository;

namespace E_HospitalServer.DataAccess.Repositories;

internal sealed class AppointmentRepository : Repository<Appointment, ApplicationDbContext>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
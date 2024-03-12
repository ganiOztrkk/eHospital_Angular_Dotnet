using AutoMapper;
using E_HospitalServer.Business.Services;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Enums;
using E_HospitalServer.Entities.Models;
using E_HospitalServer.Entities.Repositories;
using E_HostpitalServer.Core.Utilities.Results;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_HospitalServer.DataAccess.Services;

internal sealed class AppointmentService(
    UserManager<User> userManager,
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IAppointmentService
{
    public async Task<Result<string>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        User? doctor = await userManager.Users.Include(p=> p.DoctorDetail).FirstOrDefaultAsync(p=> p.Id == request.DoctorId, cancellationToken: cancellationToken);
        if(doctor is null || doctor.UserType is not UserType.Doctor)
        {
            return Result<string>.Failure(500,"Doctor not found");
        }

        string day = request.StartDate.ToString("dddd");

        if(!doctor.DoctorDetail!.WorkingDays.Contains(day))
        {
            return Result<string>.Failure(500,"Doctor is not working that day");
        }

        IQueryable<Appointment> appointments =
            appointmentRepository
                .GetWhere(p => p.DoctorId == request.DoctorId);

        DateTime startDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        DateTime endDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

        bool isDoctorHaveAppointment = true;

        isDoctorHaveAppointment = await appointments
            .AnyAsync(p =>                        
                    ((p.StartDate < endDate && p.StartDate >= startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                     (p.EndDate > startDate && p.EndDate <= endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                     (p.StartDate >= startDate && p.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde tamamen
                     (p.StartDate <= startDate && p.EndDate >= endDate)), // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                cancellationToken);      

        if(isDoctorHaveAppointment)
        {
            return Result<string>.Failure(500,"Doctor is not available in that time");
        }

        Appointment appointment = mapper.Map<Appointment>(request);

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Create appointment is succedded");
    }

    public async Task<Result<string>> CompleteAsync(CompleteAppointmentDto request, CancellationToken cancellationToken)
    {
        var appointment = 
            await appointmentRepository
            .GetByExpressionWithTrackingAsync(x => x.Id ==request.AppointmentId, cancellationToken);
        if (appointment is null)
        {
            return Result<string>.Failure(500,"Appointment is null.");
        }
        if (appointment.IsItFinished)
        {
            return Result<string>.Failure(500,"Appointment is already finished. You cannot close again.");
        }
        appointment.EpicrisisReport = request.EpicrisisReport;
        appointment.IsItFinished = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Appointment is completed");

    }

    public async Task<Result<List<Appointment>>> GetAllByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken)
    {
        List<Appointment> appointments = 
            await appointmentRepository
                .GetWhere(p=> p.DoctorId == doctorId)
                .Include(p=> p.Doctor)
                .Include(p=> p.Patient)
                .OrderBy(p=> p.StartDate)
                .ToListAsync(cancellationToken: cancellationToken);

        return Result<List<Appointment>>.Succeed(appointments);
    }
}
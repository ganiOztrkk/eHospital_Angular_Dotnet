using E_HospitalServer.Business.Services;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.WebApi.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HospitalServer.WebApi.Controllers;

public sealed class AppointmentsController(
    IUserService userService,
    IAppointmentService appointmentService) : ApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePatient(CreatePatientDto request, CancellationToken cancellationToken)
    {
        var response = await userService.CreatePatientAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        var response = await appointmentService.CreateAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CompleteAppointment(CompleteAppointmentDto request, CancellationToken cancellationToken)
    {
        var response = await appointmentService.CompleteAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("{doctorId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllByDoctorId(Guid doctorId, CancellationToken cancellationToken)
    {
        var response = await appointmentService.GetAllByDoctorIdAsync(doctorId, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
using E_HospitalServer.Business.Services;
using E_HospitalServer.Entities.Models;
using E_HospitalServer.WebApi.Abstractions;
using E_HostpitalServer.Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HospitalServer.WebApi.Controllers;

public sealed class DoctorsController(
    IUserService userService) : ApiController
{

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
    {
        Result<List<User>> response = await userService.GetAllDoctorsAsync(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
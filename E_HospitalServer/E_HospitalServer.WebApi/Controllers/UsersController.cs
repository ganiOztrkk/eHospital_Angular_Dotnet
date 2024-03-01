using E_HospitalServer.Business.Services;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.WebApi.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HospitalServer.WebApi.Controllers;


public class UsersController(
    IUserService userService) : ApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserDto request, CancellationToken cancellationToken)
    {
        var response = await userService.CreateUserAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
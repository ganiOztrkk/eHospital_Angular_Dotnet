using Microsoft.AspNetCore.Mvc;

namespace E_HospitalServer.WebApi.Abstractions;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
}
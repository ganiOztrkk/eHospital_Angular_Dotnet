using E_HospitalServer.Business.Services;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.WebApi.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HospitalServer.WebApi.Controllers
{
    public class AuthController(IAuthService authService) : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var response = await authService.LoginAsync(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenByRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var response = await authService.GetTokeByRefreshTokenAsync(refreshToken, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
        
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SendConfirmMail(string email, CancellationToken cancellationToken)
        {
            var response = await authService.SendConfirmEmailAsync(email, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int emailConfirmCode, CancellationToken cancellationToken)
        {
            var response = await authService.ConfirmVerificationEmailAsync(emailConfirmCode, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SendForgotPasswordEmail(string emailOrUserName, CancellationToken cancellationToken)
        {
            var response = await authService.SendForgotPasswordEmailAsync(emailOrUserName, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordWithForgotPasswordCode(ChangePasswordWithForgotPasswordCodeDto request, CancellationToken cancellationToken)
        {
            var response = await authService.ChangePasswordWithForgotPasswordCodeAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using StockApp.API.Resources;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAuditService _userAuditService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IEmailService _emailService;

        public UserController(IUserRepository userRepository, IUserAuditService userAuditService, IStringLocalizer<SharedResource> localizer, IEmailService emailService)
        {
            _userRepository = userRepository;
            _userAuditService = userAuditService;
            _localizer = localizer;
            _emailService = emailService;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userRepository.GetByUsernameAsync(forgotPasswordDto.Username);
            if (user == null || user.Email != forgotPasswordDto.Email)
            {
                return NotFound(_localizer["UserNotFound"]);
            }

            var resetToken = GenerateResetToken();
            // Enviar email com token de redefinição de senha
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Your reset token is: {resetToken}");

            // Salvar o token no usuário (dependendo da implementação do repositório)
            user.PasswordResetToken = resetToken;
            await _userRepository.UpdateAsync(user);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userRepository.GetByUsernameAsync(resetPasswordDto.Username);
            if (user == null || user.PasswordResetToken != resetPasswordDto.Token)
            {
                return NotFound(_localizer["InvalidToken"]);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            user.PasswordResetToken = null;
            await _userRepository.UpdateAsync(user);

            return Ok();
        }

        private string GenerateResetToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

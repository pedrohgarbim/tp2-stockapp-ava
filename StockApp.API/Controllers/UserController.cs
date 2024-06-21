using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Application.DTOs;
using Microsoft.Extensions.Localization;
using StockApp.API.Resources;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAuditService _userAuditService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UserController(IUserRepository userRepository, IUserAuditService userAuditService, IStringLocalizer<SharedResource> localizer)
        {
            _userRepository = userRepository;
            _userAuditService = userAuditService;
            _localizer = localizer;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var user = new User
            {
                Username = userRegisterDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
                Role = userRegisterDto.Role
            };

            await _userRepository.AddAsync(user);
            _userAuditService.LogUserAction(user.Username, "User registered");
            return Ok(user);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(userLoginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                return Unauthorized(_localizer["InvalidLogin"]);
            }

            _userAuditService.LogUserAction(user.Username, "User logged in");
            return Ok();
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string username, [FromBody] UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound(_localizer["UserNotFound"]);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
            user.Role = userUpdateDto.Role;

            await _userRepository.AddAsync(user);
            _userAuditService.LogUserAction(user.Username, "User updated");

            return NoContent();
        }
    }
}

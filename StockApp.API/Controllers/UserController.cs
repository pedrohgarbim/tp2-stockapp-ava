using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            return Ok();
        }

    }
}

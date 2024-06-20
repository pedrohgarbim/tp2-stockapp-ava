using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using Microsoft.AspNetCore.Authentication;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _authenticationService.AuthenticateAsync(null, userLoginDto.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}

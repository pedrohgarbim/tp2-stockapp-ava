using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("mfa auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMfaService _mfaService;

        public AuthController(IMfaService mfaService)
        {
            _mfaService = mfaService;
        }

        [HttpPost("generate-otp")]
        public ActionResult<string> GenerateOtp()
        {
            var otp = _mfaService.GenerateOtp();
            return Ok(otp);
        }

        [HttpPost("validate-otp")]
        public ActionResult<bool> ValidateOtp([FromBody] string userOtp)
        {
            string storedOtp;
            var isValid = _mfaService.ValidateOtp(userOtp, out storedOtp);
            return Ok(isValid);
        }
    }
}

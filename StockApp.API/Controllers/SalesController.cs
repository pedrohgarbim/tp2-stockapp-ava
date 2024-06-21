using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api / [controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public SalesController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet("calculate-tax")]
        public IActionResult CalculateTax(decimal amount) 
        {
            var tax = _taxService.CalculateTax(amount);
            return Ok(tax);
        }
    }
}

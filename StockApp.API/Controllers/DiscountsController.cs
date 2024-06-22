using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost("apply-discount")]
        public IActionResult ApplyDiscount([FromBody] ApplyDiscountDto dto)
        {
            var discuntedPrice = _discountService.ApplyDiscount(dto.Price, dto.DiscountPercentage);
            return Ok (discuntedPrice);
        }
    }

    public class ApplyDiscountDto
    {
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set;}
    }
}

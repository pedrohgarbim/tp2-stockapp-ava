using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnsController : ControllerBase
    {
        private readonly IReturnService _returnService;
        public ReturnsController(IReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnProduct(ReturnProductDTO returnProductDTO)
        {
            var res = await _returnService.ProcessReturnAsync(returnProductDTO);
            if (!res)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

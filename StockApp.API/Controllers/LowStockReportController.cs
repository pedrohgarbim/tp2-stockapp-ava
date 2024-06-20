using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class LowStockReportController : ControllerBase
    {
        private readonly ILowStockReportService _lowStockReportService;
        
        public LowStockReportController(ILowStockReportService lowStockReportService)
        {
            _lowStockReportService = lowStockReportService;
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<LowStockProductDTO>>> GetLowStock([FromQuery] int threshold)
        {
            var report = await _lowStockReportService.GetLowStockReport(threshold);
            return Ok(report);
        }
    }
}

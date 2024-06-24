using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesAnalysisController : ControllerBase
    {
        private readonly ISalesPerformanceAnalysisService _salesPerformanceAnalysisService;

        public SalesAnalysisController(ISalesPerformanceAnalysisService salesPerformanceAnalysisService)
        {
            _salesPerformanceAnalysisService = salesPerformanceAnalysisService;
        }

        [HttpGet("analyze")]
        public async Task<IActionResult> AnalyzeSalesPerformace()
        {
            var performace = await _salesPerformanceAnalysisService.AnalyzePerformacesAsync();
            return Ok(performace);
        }
    }
}

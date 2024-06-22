using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketTrendController : ControllerBase
    {
        private readonly IMarketTrendAnalysisService _marketTrendAnalysisService;
        
        public MarketTrendController(IMarketTrendAnalysisService marketTrendAnalysisService)
        {
            _marketTrendAnalysisService = marketTrendAnalysisService;
        }

        [HttpGet("analyze")]
        public async Task<ActionResult<MarketTrendDto>> AnalyzeTrends(string trend, string prediction)
        {
            var result = await _marketTrendAnalysisService.AnalyzeTrendsAsync(trend, prediction);
            return Ok(result);
        }
    }
}

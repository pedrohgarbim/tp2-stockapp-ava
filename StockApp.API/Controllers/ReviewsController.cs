using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ReviewsController : ControllerBase
    {
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public ReviewsController(ISentimentAnalysisService sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        [HttpPost("analyze-sentiment")]
        public IActionResult AnalyzeSentiment([FromBody] string review)
        {
            var sentiment = _sentimentAnalysisService.AnalyzeSentimentAsync(review);
            return Ok(sentiment);
        }
    }
}

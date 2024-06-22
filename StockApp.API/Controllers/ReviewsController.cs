using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ReviewsController : ControllerBase
    {
        private readonly ISentimentAnalysisService _sentimentAnalysisService;
        private readonly IReviewModerationService _reviewModerationService;

        public ReviewsController
            (ISentimentAnalysisService sentimentAnalysisService,
            IReviewModerationService reviewModerationService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
            _reviewModerationService = reviewModerationService;
        }

        [HttpPost("analyze-sentiment")]
        public IActionResult AnalyzeSentiment([FromBody] string review)
        {
            var sentiment = _sentimentAnalysisService.AnalyzeSentimentAsync(review);
            return Ok(sentiment);
        }

        [HttpPost("Moderate")]
        public IActionResult ModerateReview([FromBody] string review)
        {
            var isApproved = _reviewModerationService.ModerateReview(review);
            if (isApproved)
            {
                return Ok("Review aprovado");
            } else
            {
                return BadRequest("Review contém conteúdo inapropriado.");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackRequest resquest)
        {
            if (string.IsNullOrEmpty(resquest.UserId) || string.IsNullOrEmpty(resquest.Feedback)) 
            {
                return BadRequest("UserID and Feedback are required.");
            }

            await _feedbackService.SubmitFeedbackAsync(resquest.UserId, resquest.Feedback);
            return Ok("Feedback submitted successfully.");
        }
    }

    public class FeedbackRequest
    {
        public string UserId { get; set; }
        public string Feedback { get; set; }
    }
}


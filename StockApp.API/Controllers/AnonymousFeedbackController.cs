using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api / [controller]")]
    public class AnonymousFeedbackController : ControllerBase
    {
        private readonly IAnonymousFeedbackService _anonymousFeedbackService;

        public AnonymousFeedbackController(IAnonymousFeedbackService anonymousFeedbackService)
        {
            _anonymousFeedbackService = anonymousFeedbackService;
        }

        [HttpPost("collect")]
        public async Task<IActionResult> CollectFeddback([FromBody] AnonymousFeedbackRequest request)
        {
            await _anonymousFeedbackService.CollectFeedbackAsync(request.Feedback);
            return Ok("Anonymous feedback collect successfully");
        }
    }

    public class AnonymousFeedbackRequest
    {
        public string Feedback { get; set; }
    }
}

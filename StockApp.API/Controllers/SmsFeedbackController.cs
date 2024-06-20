using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api / [controller]")]
    public class SmsFeedbackController : ControllerBase
    {
        private readonly ISmsFeedbackService _smsFeedbackService;

        public SmsFeedbackController(ISmsFeedbackService smsFeedbackService)
        {
            _smsFeedbackService = smsFeedbackService;
        }

        [HttpPost("collect")]
        public async Task<IActionResult> CollectFeedback([FromBody] SmsFeedbackRequest request)
        {
            await _smsFeedbackService.CollectFeedbackAsync(request.PhoneNumber, request.Feedback);
            return Ok("SMS feedbacl request sent successfully.");
        }
    }

    public class SmsFeedbackRequest
    {
        public string PhoneNumber { get; set; }
        public string Feedback { get; set; }
    }


}

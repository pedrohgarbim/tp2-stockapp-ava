using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IWebhookService _webhookService;

        public NotificationController(IWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] WebhookDto webhookDto)
        {
            await _webhookService.SendWebhookNotification(webhookDto);
            return Ok();
        }


    }
}

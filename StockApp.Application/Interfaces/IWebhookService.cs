using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IWebhookService
    {
        Task SendWebhookNotification(WebhookDto webhookDto);
    }
}

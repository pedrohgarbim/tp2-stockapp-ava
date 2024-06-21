using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using Newtonsoft.Json;

namespace StockApp.Application.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly HttpClient _httpClient;

        public WebhookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendWebhookNotification(WebhookDto webhookDto)
        {
            var jsonPayload = JsonConvert.SerializeObject(webhookDto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // URL do sistema externo que receberá a notificação do webhook
            var webhookUrl = "https://example.com/webhook-endpoint";

            await _httpClient.PostAsync(webhookUrl, content);
        }
    }
}

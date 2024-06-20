using AutoMapper;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;


namespace StockApp.Application.Services
{
    public class SmsService : ISmsService
    {
        private readonly string _smsApiUrl;

        public SmsService(IConfiguration configuration)
        {
            _smsApiUrl = configuration["SmsApiUrl"];
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            using var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("phoneNumber", phoneNumber),
                new KeyValuePair<string, string>("message", message)
            });

            var response = await httpClient.PostAsync(_smsApiUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}

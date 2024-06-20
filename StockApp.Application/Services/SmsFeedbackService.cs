using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class SmsFeedbackService : ISmsFeedbackService
    {
        private readonly ISmsService _smsService;

        public SmsFeedbackService(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public async Task CollectFeedbackAsync(string phoneNumber, string feedback)
        {
            string message = $"We would love to hear your feedback: {feedback}";
            await _smsService.SendSmsAsync(phoneNumber, feedback) ;
        }
    }
}

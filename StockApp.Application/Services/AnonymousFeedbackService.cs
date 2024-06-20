using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class AnonymousFeedbackService : IAnonymousFeedbackService
    {
        public async Task CollectFeedbackAsync(string feedback)
        {
            await Task.CompletedTask;
        }
    }
}

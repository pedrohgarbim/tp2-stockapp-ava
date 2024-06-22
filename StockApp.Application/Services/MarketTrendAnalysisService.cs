using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class MarketTrendAnalysisService : IMarketTrendAnalysisService
    {
        public async Task<MarketTrendDto> AnalyzeTrendsAsync(string trend, string prediction)
        {
            return new MarketTrendDto
            {
                Trend = trend,
                Prediction = prediction
            };
        }
    }
}

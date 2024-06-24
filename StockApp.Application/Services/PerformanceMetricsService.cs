using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.Interfaces;
using StockApp.Application.DTOs;

namespace StockApp.Application.Services
{
    public class PerformanceMetricsService : IPerformanceMetricsService
    {
        public async Task<PerformanceMetricsDto> GetMetricsAsync()
        {
            return new PerformanceMetricsDto
            {
                CpuUsage = 75.5,
                MemoryUsage = 2048,
                ResponseTime = 250
            };
        }
    }
}

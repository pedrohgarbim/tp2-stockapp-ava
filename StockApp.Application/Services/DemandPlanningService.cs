using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Application.Services
{
    public class DemandPlanningService : IDemandPlanningService
    {
        public async Task<DemandForecastDto> ForecastDemandAsync()
        {
            return new DemandForecastDto
            {
                ProductId = 1,
                ForecastDemand = 100
            };
        }
    }
}

using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class SalesPerformanceAnalysisService : ISalesPerformanceAnalysisService
    {
        private readonly IOrderRepository _orderRepository;

        public SalesPerformanceAnalysisService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<SalesPerformanceDto> AnalyzePerformacesAsync()
        {
            var totalSales =  await _orderRepository.GetSales();
            var totalOrders = await _orderRepository.GetCount();
            var avarageOrderValue = totalOrders > 0 ? totalSales/totalOrders : 0;

            return new SalesPerformanceDto
            {
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                AvarageOrdersValue = avarageOrderValue
            };
        }
    }
}

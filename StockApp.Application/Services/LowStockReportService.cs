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
    public class LowStockReportService : ILowStockReportService
    {
        private readonly IProductRepository _productRepository;

        public LowStockReportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<LowStockProductDTO>> GetLowStockReport(int threshold)
        {
            var products = await  _productRepository.GetLowStockAsync(threshold);
            return products.Select(P => new LowStockProductDTO
            {
                ProductId = P.Id,
                ProductName = P.Name,
                StockQuantity = P.Stock,
                Threshold = threshold,
            });
        }
    }
}

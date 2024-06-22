using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class JustInTimeInventoryService : IJustInTimeInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public JustInTimeInventoryService(IProductRepository productRepository, IOrderRepository order)
        {
            _productRepository = productRepository;
            _orderRepository = order;
        }
        public async Task OptimizeInventoryAsync()
        {
            var products = await _productRepository.GetAllAsync();
            foreach (var product in products)
            {
                var optimalStockLevel = await CalculateOptimalStockLevelAsync(product);
                product.Stock = optimalStockLevel;
                await _productRepository.UpdateAsync(product);
            }
        }

        private async Task<int> CalculateOptimalStockLevelAsync(Product product)
        {
            var orders = await _orderRepository.GetByProductAsync(product.Id);

            int totalSold = orders.Sum(o => o.Quantity);
            return totalSold;
        }
    }
}

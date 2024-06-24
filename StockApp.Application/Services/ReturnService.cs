using StockApp.Application.DTOs;
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
    public class ReturnService : IReturnService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public ReturnService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository; 
        }

        public async Task<bool> ProcessReturnAsync(ReturnProductDTO returnProductDTO)
        {
            var orders = _orderRepository.GetAll();
            Order order = null;
            if (!orders.Any(o => o.Id == returnProductDTO.OrderId))
            {
                return false;
            }
            else
                order = orders.Where(o=> o.Id == returnProductDTO.OrderId).First();

            if(order.Quantity != returnProductDTO.Quantity)
            {
                return false;
            }
            
            if(order.ProductId != returnProductDTO.ProductId)
            {
                return false;
            }
            
            var product =  order.Product;
            product.Stock = returnProductDTO.Quantity;

            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}

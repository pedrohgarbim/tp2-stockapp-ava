using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IEmailNotificationService _emailNotificationService;
        private const string CacheKeyPrefix = "Product_";

        public ProductService(IProductRepository productRepository, IMapper mapper, IDistributedCache cache, IEmailNotificationService emailNotificationService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cache = cache;
            _emailNotificationService = emailNotificationService;
        }

        public async Task Add(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var cacheKey = $"{CacheKeyPrefix}{GetProducts}";

            var cachedProducts = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return null; // nao implementado
            }
            var productsEntity = await _productRepository.GetAllAsync();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            };

            await _cache.SetStringAsync(cacheKey, cachedProducts, cacheOptions);
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetProductById(int? id)
        {
            var productEntity = _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task Remove(int? id)
        {
            var productEntity = _productRepository.GetByIdAsync(id).Result;
            await _productRepository.DeleteAsync(id);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(productEntity);
        }

        public async Task BulkUpdateAsync(IEnumerable<Product> products)
        {
            await _productRepository.BulkUpdateAsync(products);
        }
    }
}

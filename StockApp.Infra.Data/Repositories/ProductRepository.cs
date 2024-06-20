using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace StockApp.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;
        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _productContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(int? id)
        {
            var products = await _productContext.Products.FindAsync(id);
            if (products != null)
            {
                _productContext.Products.Remove(products);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold)
        {
            return await _productContext.Products
                .Where(p => p.Stock < threshold)
                .ToListAsync();
        }
        
        public async Task BulkUpdateAsync(IEnumerable<Product> products)
        {
            _productContext.Products.UpdateRange(products);
            await _productContext.SaveChangesAsync();
        }
    }
}

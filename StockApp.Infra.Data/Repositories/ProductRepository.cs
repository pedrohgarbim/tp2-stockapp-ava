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

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _productContext.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string name, decimal? minPrice, decimal? maxPrice)
        {
            var query = _productContext.Products.AsQueryable();

            if(!string.IsNullOrEmpty(name) )
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if(minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if(maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
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

        public async Task<IEnumerable<Product>> SearchAsync(string query, string sortBy, bool descending)
        {
            IQueryable<Product> queryable = _productContext.Products;

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(p => 
                p.Name.Contains(query) || 
                p.Description.Contains(query));
            }

            if(!string.IsNullOrEmpty(sortBy))
            {
                Func<Product, object> orderByFunc = sortBy.ToLower() switch
                {
                    "name" => (Func<Product, object>)(p => p.Name),
                    "price" => p => p.Price,
                    "stock" => p => p.Stock,
                    _ => p => p.Id
                };

                queryable = (IQueryable<Product>)(descending ? queryable.OrderByDescending(orderByFunc) : queryable.OrderBy(orderByFunc));
            }
            else
            {
                queryable = queryable.OrderBy(p => p.Id);
            }

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice)
        {
            var query = _productContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (minPrice > 0)
            {
                query = query.Where(p => p.Price >= minPrice);
            }


            if (maxPrice > 0)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _productContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}

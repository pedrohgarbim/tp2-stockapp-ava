using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int? id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> Remove(Product product);
        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
        Task BulkUpdateAsync(IEnumerable<Product> products);
    }
}

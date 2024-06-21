using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int? id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(int? id);
        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
        Task BulkUpdateAsync(IEnumerable<Product> products);
        Task<IEnumerable<Product>> SearchAsync(string query, string sortBy, bool descending);
        Task <IEnumerable<Product>> GetFilteredAsync(string name, decimal? minPrice, decimal? maxPrice);
    }
}

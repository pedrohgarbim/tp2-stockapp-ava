using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.API.GraphQL
{
    public class Query
    {
        public async Task<IQueryable<Product>> GetProducts([Service] IProductRepository productRepository)
        {
            var products = await productRepository.GetAllAsync();
            return products.AsQueryable();
        }
    }
}

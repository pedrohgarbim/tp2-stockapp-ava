using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.API.GraphQL
{
    public class Mutation
    {
        public async Task<Product> AddProduct([Service] IProductRepository productRepository, Product product)
        {
            await productRepository.AddAsync(product);
            return product;
        }
    }
}

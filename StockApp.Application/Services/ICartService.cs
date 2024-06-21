using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(CartItem cartItem);
        Task RemoveFromCartAsync(int productId);
        Task<List<CartItem>> GetCartItemsAsync();
    }
}

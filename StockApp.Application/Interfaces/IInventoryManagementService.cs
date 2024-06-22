using StockApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IInventoryManagementService
    {
        Task AddProductAsync(ProductDTO product);
        Task RemoveProductAsync(int productId);
        Task UpdateProductAsync(ProductDTO product);

    }
}

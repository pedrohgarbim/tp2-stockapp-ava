using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryManagementService _inventoryManagementService;
        private readonly IJustInTimeInventoryService _justInTimeInventoryService;

        public InventoryController(
            IInventoryService inventoryService,
            IInventoryManagementService inventoryManagementService,
            IJustInTimeInventoryService justInTimeInventoryService)
        {
            _inventoryService = inventoryService;
            _inventoryManagementService = inventoryManagementService;
            _justInTimeInventoryService = justInTimeInventoryService;
        }

        [HttpPost("replenish")]
        public async Task<IActionResult> ReplenishStock()
        {
            await _inventoryService.ReplenishStockAsync();
            return Ok();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
        {
            await _inventoryManagementService.AddProductAsync(productDto);
            return Ok("Produto adicionado com sucesso");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            await _inventoryManagementService.RemoveProductAsync(productId);
            return Ok("Produto removido com sucesso");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDto)
        {
            await _inventoryManagementService.UpdateProductAsync(productDto);
            return Ok("Produto atualizado com sucesso");
        }
        [HttpPost("JIT")]
        public async Task<IActionResult> OptimizeInventory()
        {
            await _justInTimeInventoryService.OptimizeInventoryAsync();
            return Ok();
        }

    }
}

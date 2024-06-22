using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJustInTimeInventoryService _justInTimeInventoryService;

        public StockController(ApplicationDbContext context, IJustInTimeInventoryService justInTimeInventoryService)
        {
            _context = context;
            _justInTimeInventoryService = justInTimeInventoryService;
        }

        [HttpGet("dashboard-stock")]
        public async Task<IActionResult> GetDashboardStockData()
        {
            var dashboardData = new DashboardStockDto
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalStockValue = await _context.Products.SumAsync(p => p.Stock * p.Price),
                LowStockProducts = await _context.Products
                .Where(p => p.Stock < 10)
                .Select(p => new ProductStockDto
                {
                    ProductName = p.Name,
                    Stock = p.Stock,
                })
                .ToListAsync()
            };
            return Ok(dashboardData);
        }

        [HttpPost("JIT")]
        public async Task<IActionResult> OptimizeInventory()
        {
            await _justInTimeInventoryService.OptimizeInventoryAsync();
            return Ok();
        }
    }
}

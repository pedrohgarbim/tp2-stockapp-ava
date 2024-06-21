using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Infra.Data.Context;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}

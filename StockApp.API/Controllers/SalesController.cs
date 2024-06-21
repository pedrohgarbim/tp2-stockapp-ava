using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api / [controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ITaxService _taxService;
        private readonly ApplicationDbContext _context;

        public SalesController(ITaxService taxService, ApplicationDbContext context)
        {
            _taxService = taxService;
            _context = context;
        }

        [HttpGet("calculate-tax")]
        public IActionResult CalculateTax(decimal amount) 
        {
            var tax = _taxService.CalculateTax(amount);
            return Ok(tax);
        }

        [HttpGet("dashboard-sales")]
        public async Task<IActionResult> GetDashBoardSalesData()
        {
            var dashboardData = new DashboardSalesDto
            {
                TotalSales = await _context.Orders.SumAsync(o => o.Quantity * o.Price),
                TotalOrders = await _context.Orders.CountAsync(),
                TopSellingProducts = await _context.Products
                .OrderByDescending(p => p.Orders.Sum(o => o.Quantity))
                .Take(5)
                .Select(p => new ProductSalesDto
                {
                    ProductName = p.Name,
                    TotalSold = p.Orders.Sum(p => p.Quantity)
                })
                .ToListAsync()
            };

            return Ok(dashboardData);
        }
    }
}

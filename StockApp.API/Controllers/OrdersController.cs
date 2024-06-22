using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITaxService _taxService;

        public OrdersController(ApplicationDbContext context, ITaxService taxService)
        {
            _context = context;
            _taxService = taxService;
        }

        [HttpGet("tax-report")]
        public async Task<IActionResult> GetTaxReport()
        {
            var sales = await _context.Orders.ToListAsync();
            var taxReport =sales.Select(s => new TaxReportDto
            {
                OrderId = s.Id,
                TaxAmount = _taxService.CalculateTax(s.TotalAmount)
            }).ToList();

            return Ok(taxReport);
        }
    }
}

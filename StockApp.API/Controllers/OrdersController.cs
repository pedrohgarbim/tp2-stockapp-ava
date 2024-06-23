using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
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
        private readonly IOrdersPredictionService _ordersPredictionService;

        public OrdersController(ApplicationDbContext context, ITaxService taxService, IOrdersPredictionService ordersPredictionService)
        {
            _context = context;
            _taxService = taxService;
            _ordersPredictionService = ordersPredictionService;
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

        [HttpGet("predict")]
        public IActionResult PredictOrders(int productId, int month, int year)
        {
            var predict = _ordersPredictionService.PredictOrders(productId, month, year);
            return Ok(predict);
        }

    }
}

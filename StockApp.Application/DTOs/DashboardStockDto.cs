using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class DashboardStockDto
    {
        public int TotalProducts { get; set; }
        public decimal TotalStockValue { get; set; }
        public List<ProductStockDto> LowStockProducts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public  class SalesPerformanceDto
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AvarageOrdersValue { get; set; }
    }
}

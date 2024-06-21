using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class TaxReportDto
    {
        public int OrderId { get; set; }
        public decimal TaxAmount { get; set; }
    }
}

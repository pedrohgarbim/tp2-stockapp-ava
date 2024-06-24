using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class DemandForecastDto
    {
        public int ProductId { get; set; }
        public int ForecastDemand { get; set; }
    }
}

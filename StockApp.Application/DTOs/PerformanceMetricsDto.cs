using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class PerformanceMetricsDto
    {
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public int ResponseTime { get; set; }
    }
}

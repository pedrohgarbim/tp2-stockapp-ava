using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FastReport.DataVisualization.Charting;
using StockApp.Domain.Interfaces;


namespace StockApp.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IProductRepository _productRepository;
        public ReportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public  byte[] GenerateStockReport()
        {
            var chart = new Chart
            {
                Width = 600,
                Height = 400
            };
            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            var orders =  _productRepository.GetAllAsync().Result;

            var series = new Series
            {
                Name = "Stock",
                ChartType = SeriesChartType.Bar
            };

            foreach (var item in orders)
            {
                series.Points.AddXY(item.Name, item.Stock);
            }

            using(var stream = new MemoryStream())
            {
                chart.SaveImage(stream, ChartImageFormat.Png);
                return stream.ToArray();
            }
            
        }
    }
}

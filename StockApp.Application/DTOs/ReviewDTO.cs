using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class ReviewDTO
    {
        public int Rating { get; set; } 
        public string? Comment { get; set; }
    }
}

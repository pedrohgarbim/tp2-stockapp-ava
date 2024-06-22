using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class SalesDTO
    {
       public int ProductId { get; set; }
       public int Month { get; set; }
       public int Year { get; set; }  
       public int Sales { get; set; }
    }
}

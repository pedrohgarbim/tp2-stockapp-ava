using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class ReturnProductDTO
    {
        public int OrderId { get; set; }
        public int ProductId{ get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
    }
}

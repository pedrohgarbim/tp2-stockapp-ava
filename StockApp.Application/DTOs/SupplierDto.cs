using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ContactEmail { get; set; }
        public int EvaluationScore { get; set; }
        public string PhoneNumber { get; internal set; }
    }
}

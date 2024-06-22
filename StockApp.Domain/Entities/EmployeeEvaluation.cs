using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class EmployeeEvaluation
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int EvaluationScore { get; set; }
        public string FeedBack { get; set; }
        public Employee Employee { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class WebhookDto
    {
        public string Event {  get; set; }
        public string Payload { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ClientData
    {
        public int ClientId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string MarketingData { get; set; }
    }
}

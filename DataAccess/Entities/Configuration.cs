using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Configuration
    {
        public int Id { get; set; }
        public string ConfigurationName { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public Template Template { get; set; }
    }
}

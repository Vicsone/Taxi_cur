using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi
{
    public class Request
    {
        public int Id { get; set; }
        
        public string AddressFrom { get; set; }
        public string AddressWhere { get; set; }
        public int ClientId { get; set; } 
        public int OperatorId { get; set; }
        public User Client { get; set; }
        public User? Operator { get; set; }
        public DateTime Date { get; set; }
    }
}

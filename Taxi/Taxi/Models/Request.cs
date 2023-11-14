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
        
        public string startpos { get; set; }
        public string nextpos { get; set; }
        public int client_id { get; set; }
    }
}

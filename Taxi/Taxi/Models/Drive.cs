using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi
{
    public class Drive
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int DriverId { get; set; }
        public int RequestId { get; set; }
        public Status Status { get; set; }
        public Driver Driver { get; set; }
        public Request Request { get; set; }
    }
}
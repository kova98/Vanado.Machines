using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Models
{
    public class UnresolvedFaultViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MachineName { get; set; }
        public int Priority { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}

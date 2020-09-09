using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Models
{
    public class Fault
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public long MachineId { get; set; }
        public string Description { get; set; }
        public FaultStatus Status { get; set; } = FaultStatus.Unresolved;
        public int Priority { get; set; } = 1;
        public DateTimeOffset Date { get; set; } = DateTime.Now;
    }
}

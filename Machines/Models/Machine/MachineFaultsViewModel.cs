using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Models
{
    public class MachineFaultsViewModel
    {
        public string MachineName { get; set; }

        public IEnumerable<Fault> Faults { get; set; }
    }
}
